using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDTORequest createAccountDTORequest)
        {
             try
             {
                CreateAccountDTOResponse user = await _authenticationService.Register(createAccountDTORequest);

                return CustomResult("Register Success",user, HttpStatusCode.OK);

             }
             catch (Exception e)
             {
                  return CustomResult(e.Message, HttpStatusCode.InternalServerError);
             }
            
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTORequest loginDtoRequest)
        {
            try
            {
                (string, LoginDTOResponse) tuple = await _authenticationService.Login(loginDtoRequest);
                if (tuple.Item1 == null)
                {
                    return Unauthorized();
                }

                Dictionary<string, object> result = new()
                {
                    { "token", tuple.Item1 },
                    { "user", tuple.Item2 ?? null }
                };
                return CustomResult("Login Success",result, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return CustomResult(e.Message, HttpStatusCode.InternalServerError);
            }

            
        }
        
    }
}
