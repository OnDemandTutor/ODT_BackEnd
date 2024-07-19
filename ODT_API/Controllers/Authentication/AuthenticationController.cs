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
using ODT_Repository.Entity;
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
        private readonly IEmailConfig _emailConfig;
        private readonly IUserService _userService;


        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IUnitOfWork unitOfWork, IEmailConfig emailConfig, IUserService userService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailConfig = emailConfig;
            _userService = userService;
        }
        /*[HttpPost("Register")]
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
            
        }*/

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                CreateAccountDTOResponse user = await _authenticationService.Register(registerRequest);

                return CustomResult("Register Success", user, HttpStatusCode.OK);

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

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(UserForgotPassDTO forgotPassUser)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(forgotPassUser.Email);

            if (user == null)
            {
                return BadRequest(new ResponseDTO
                {
                    Success = false,
                    Message = "Could not send link to email, please try again. \nYour email does not exist in system."
                });
            }

            var token = Tools.Authentication.GenerateRandomString(10);
            var forgotPasswordLink = $"http://localhost:3000/resetpass?token={token}&email={user.Email}";
            var tokenEntity = new Token
            {
                TokenValue = token,
                UserId = user.Id
            };
            await _authenticationService.SaveToken(tokenEntity);
            Console.WriteLine("Link: " + forgotPasswordLink);
            var message = new EmailDTO
            (
                new string[] { user.Email },
                "Forgot Password Link!",
                forgotPasswordLink!
            );

            _emailConfig.SendEmail(message);

            return Ok(new ResponseDTO
            {
                Success = true,
                Message = $"Password changed request is sent on your Email {user.Email}.Please open your email and click the link."
            });
        }
        [HttpPost("ResetPassword")]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassAsync(UserResetPassDTO userReset)
        {
            var result = await _authenticationService.ResetPassAsync(userReset);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
