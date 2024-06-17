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

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
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

        var user = _mapper.Map<User>(createAccountRequest);
        user.Password = EncryptPassword.Encrypt(createAccountRequest.Password);
        user.Status = true;
        user.CreateDate = DateTime.Now;
        //user.Avatar = null;
        await _unitOfWork.UserRepository.AddAsync(user);
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
            includeProperties:"Role",
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
}