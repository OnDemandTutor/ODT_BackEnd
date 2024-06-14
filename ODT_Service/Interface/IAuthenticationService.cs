using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface;

public interface IAuthenticationService
{
    Task<CreateAccountDTOResponse> Register(CreateAccountDTORequest createAccountDTORequest);
    Task<(string, LoginDTOResponse)> Login(LoginDTORequest loginDtoRequest);
}
