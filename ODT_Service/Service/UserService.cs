using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Model.Enum;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Service.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Tools.Firebase _firebase;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, Tools.Firebase firebase)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _firebase = firebase;
    }
    public async Task<User> CreateUser(CreateAccountDTORequest createAccountRequest)
    {
        IEnumerable<User> checkEmail =
            await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(createAccountRequest.Email));
        IEnumerable<User> checkUsername =
            await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Username.Equals(createAccountRequest.Username));
        if (checkEmail.Count() != 0)
        {
            throw new InvalidDataException($"Email is exist");
        }

        if (checkUsername.Count() != 0)
        {
            throw new InvalidDataException($"Username is exist");
        }
        var userGender = "Order";
        switch (createAccountRequest.Gender.Trim().ToLower())
        {
            case "male": userGender = "Male"; break;
            case "female": userGender = "Female"; break;
        }

        var user = _mapper.Map<User>(createAccountRequest);
        user.Gender = userGender;
        user.Password = EncryptPassword.Encrypt(createAccountRequest.Password);
        user.Status = true;
        user.CreateDate = DateTime.Now;
        user.Avatar = "https://firebasestorage.googleapis.com/v0/b/artworks-sharing-platform.appspot.com/o/images%2Favt.jpg?alt=media&token=13ab9b47-eff9-4d33-88b0-8e7d32e0de90";
        var role = _unitOfWork.RoleRepository.Get(role => role.RoleName.Trim().ToLower() == createAccountRequest.RoleName.Trim().ToLower())
            .FirstOrDefault();
        if (role == null)
        {
            throw new CustomException.InvalidDataException("500", "This role name does not exist");
        }

        user.RoleId = role.Id;

        var roleName = await _unitOfWork.RoleRepository.GetByIdAsync(user.RoleId);
        await _unitOfWork.UserRepository.AddAsync(user);

        //add Wallet and add mentor or student based on role
        var wallet = new Wallet
        {
            UserId = user.Id,
            Balance = 0,
            Status = true
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
        return user;
    }

    public async Task<IEnumerable<UserDTOResponse>> GetAllUsers(QueryObject queryObject)
    {
        //check if QueryObject search is not null
        Expression<Func<User, bool>> filter = null;
        if (!string.IsNullOrWhiteSpace(queryObject.Search))
        {
            filter = user => user.Username.Contains(queryObject.Search);
        }

        var users = _unitOfWork.UserRepository.Get(
            filter: filter,
            includeProperties: "Role",
            pageIndex: queryObject.PageIndex,
            pageSize: queryObject.PageSize);
        if (users.IsNullOrEmpty())
        {
            throw new CustomException.DataNotFoundException("The user list is empty!");
        }

        return _mapper.Map<IEnumerable<UserDTOResponse>>(users);
    }

    public async Task<User> GetUserById(long id)
    {
        return await _unitOfWork.UserRepository.GetByIdAsync(id);
    }

    public async Task<User> UpdateUser(long id, UpdateAccountDTORequest updateAccountDTORequest)
    {
        var userToUpdate = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userToUpdate == null)
        {
            throw new InvalidDataException($"User not found");
        }
        _mapper.Map(updateAccountDTORequest, userToUpdate);
        await _unitOfWork.UserRepository.UpdateAsync(userToUpdate);

        return userToUpdate;
    }

    public string GetUserID()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
        {
            var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (claim != null)
            {
                result = claim.Value;
            }
        }
        return result;
    }

    public async Task<UserDTOResponse> GetLoginUser()
    {
        var user = GetUserFromHttpContext();


        return _mapper.Map<UserDTOResponse>(user);
    }

    public async Task<UserDTOResponse> UpdateLoginUser(UpdateAccountDTORequest updateAccountDTORequest)
    {

        var user = GetUserFromHttpContext();

        _mapper.Map(updateAccountDTORequest, user);

        IEnumerable<User> checkEmail =
            await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(updateAccountDTORequest.Email));
        if (checkEmail.Count() != 0)
        {
            throw new CustomException.InvalidDataException("Email is exist");
        }
        var userGender = "Order";
        switch (updateAccountDTORequest.Gender.Trim().ToLower())
        {
            case "male": userGender = "Male"; break;
            case "female": userGender = "Female"; break;
        }

        user.Gender = userGender;
        await _unitOfWork.UserRepository.UpdateAsync(user);
        _unitOfWork.Save();

        return _mapper.Map<UserDTOResponse>(user);
    }

    public async Task<UserDTOResponse> UpdateLoginUserAvatar(ImageRequest imageRequest)
    {
        var user = GetUserFromHttpContext();
        string[] imgExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        if (!imgExtensions.Contains(Path.GetExtension(imageRequest.Image.FileName)))
        {
            throw new CustomException.InvalidDataException("Just accept image!");
        }
        var imageurl = await _firebase.UploadImageAsync(imageRequest.Image);
        user.Avatar = imageurl;
        _unitOfWork.Save();
        return _mapper.Map<UserDTOResponse>(user);
    }

    public async Task<UserDTOResponse> ActivateUser(long id)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.Id == id).FirstOrDefault();
        if (user.Status)
        {
            throw new CustomException.InvalidDataException("This user already activated!");
        }
        user.Status = true;
        _unitOfWork.Save();
        return _mapper.Map<UserDTOResponse>(user);
    }

    public async Task<UserDTOResponse> DeactivateUser(long id)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.Id == id).FirstOrDefault();
        if (!user.Status)
        {
            throw new CustomException.InvalidDataException("This user is already disabled!");
        }
        user.Status = false;
        _unitOfWork.Save();
        return _mapper.Map<UserDTOResponse>(user);
    }

    public async Task<UserCountResponse> GetNumberOfUser()
    {
        var student = _unitOfWork.UserRepository.Get(x => x.Role.RoleName == "Student");
        var mentor = _unitOfWork.UserRepository.Get(x => x.Role.RoleName == "Mentor");

        UserCountResponse userCountResponse = new UserCountResponse
        {
            NumberOfStudent = student.Count(),
            NumberOfMentor = mentor.Count(),
            NumberOfUsers = student.Count() + mentor.Count()
        };

        return userCountResponse;
    }

    private User GetUserFromHttpContext()
    {
        var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
        var user = _unitOfWork.UserRepository.Get(u => u.Id == userId, includeProperties: "Role").FirstOrDefault();
        if (user == null)
        {
            throw new CustomException.DataNotFoundException("This user not found!");
        }

        if (user.Status == false)
        {
            throw new CustomException.InvalidDataException("This user not activated!");

        }

        return user;
    }
}