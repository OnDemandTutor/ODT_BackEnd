using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Repository.Entity;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAccountController : BaseController
    {
        private readonly IUserService _userService;
        public ManageAccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateAccountDTORequest updateUserDTORequest)
        {
            try
            {
                User user= await _userService.UpdateUser(id, updateUserDTORequest);
                return CustomResult("Update Success",user, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateAccountDTORequest createAccountDTORequest)
        {
            try
            {
                User user = await _userService.CreateUser(createAccountDTORequest);
                return CustomResult("Create User Success",user, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return CustomResult("Get User Success", user, HttpStatusCode.OK);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery]QueryObject queryObject)
        {
            try
            {
                var users = await _userService.GetAllUsers(queryObject);
            
                return CustomResult("Get All users successful", users);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
           
        }


    }
}
