using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface;

public interface IAuthenticationService
{
    //Task<CreateAccountDTOResponse> Register(CreateAccountDTORequest createAccountDTORequest);
    Task<CreateAccountDTOResponse> Register(RegisterRequest registerRequest);
    Task<(string, LoginDTOResponse)> Login(LoginDTORequest loginDtoRequest);
    Task<RegisterTutorResponse> RegisterTutor(RegisterTutorRequest registerTutorRequest);
    Task<Token> SaveToken(Token token);
    Task<ResponseDTO> ResetPassAsync(UserResetPassDTO userReset);
}
