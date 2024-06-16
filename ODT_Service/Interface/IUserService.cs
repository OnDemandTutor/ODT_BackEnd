using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface;

public interface IUserService
{
    Task<User> CreateUser(CreateAccountDTORequest createAccountRequest);
    Task<IEnumerable<UserDTOResponse>> GetAllUsers(QueryObject queryObject);
    Task<User> GetUserById(long id);
    Task<User> UpdateUser(long id, UpdateAccountDTORequest updateAccountDTORequest);
    string GetUserID();
}