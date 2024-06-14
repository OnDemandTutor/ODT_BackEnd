using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Model.Enum;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Service.Service;

public class AuthenticationService: IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
        
    public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork; 
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<CreateAccountDTOResponse> Register(CreateAccountDTORequest createAccountDTORequest)
    {
        IEnumerable<User> checkEmail =
            await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(createAccountDTORequest.Email));
        IEnumerable<User> checkUsername =
            await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Username.Equals(createAccountDTORequest.Username));
        if (checkEmail.Any())
        {
            throw new CustomException.InvalidDataException("500", "Email already exists.");
        }

        if (checkUsername.Any())
        {
            throw new InvalidDataException("Username already exists.");
        }
        var user = _mapper.Map<User>(createAccountDTORequest);
        /*			user.permission_id = (await _userPermissionRepository.GetByFilterAsync(r => r.role.Equals("Customer"))).First().id;
        */

        user.Password = EncryptPassword.Encrypt(createAccountDTORequest.Password);
        user.Status = true;
        var role = _unitOfWork.RoleRepository.Get(role => role.RoleName.Trim().ToLower() == createAccountDTORequest.RoleName.Trim().ToLower())
            .FirstOrDefault();
        if (role == null)
        {
            throw new CustomException.InvalidDataException("500", "This role name does not exist");
        }
        user.RoleId = role.Id;
        user.CreateDate = DateTime.Now.Date;






        await _unitOfWork.UserRepository.AddAsync(user);
        var roleName = await _unitOfWork.RoleRepository.GetByIdAsync(user.RoleId);
        var wallet = new Wallet
        {
            UserId = user.Id,
            Balance = 0,
            Status = false
        };
        await _unitOfWork.WalletRepository.AddAsync(wallet);
        if (roleName.RoleName == RoleName.Student.ToString())
        {
            var student = new Student();
            student.UserId = user.Id;
            await _unitOfWork.StudentRepository.AddAsync(student);

        }

        if (roleName.RoleName == RoleName.Mentor.ToString())
        {
            var mentor = new Mentor
            {
                UserId = user.Id,
                AcademicLevel = "",
                WorkPlace = "",
                VerifyStatus = false,
                OnlineStatus = OnlineStatus.Invisible.ToString(),
                Skill = "",
                Video = ""
            };


            await _unitOfWork.MentorRepository.AddAsync(mentor);

        }
        CreateAccountDTOResponse createAccountDTOResponse = _mapper.Map<CreateAccountDTOResponse>(user);
        return createAccountDTOResponse;

    }

    public async Task<(string, LoginDTOResponse)> Login(LoginDTORequest loginDtoRequest)
    {
        string hashedPass = EncryptPassword.Encrypt(loginDtoRequest.Password);
        IEnumerable<User> check = await _unitOfWork.UserRepository.GetByFilterAsync(x =>
            x.Username.Equals(loginDtoRequest.Username)
            && x.Password.Equals(hashedPass)
        );
        if (!check.Any())
        {
            throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"Username or password error");
        }

        User user = check.First();
        if (user.Status == false)
        {
            throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"User is not active");
        }

        LoginDTOResponse loginDtoResponse = _mapper.Map<LoginDTOResponse>(user);
        Authentication authentication = new(_configuration, _unitOfWork);
        string token = await authentication.GenerateJwtToken(user, 15);
        return (token, loginDtoResponse);
    }





}