using CoreApiResponse;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("GetAllRolePermission")]
        public IActionResult GetAllRolePermission([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var rps = _rolePermissionService.GetAllRolePermission(queryPbject);
                return CustomResult("Data Load Successfully", rps);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllPermissionByRoleId/{id}")]
        public async Task<IActionResult> GetAllPermissionByRoleId(long id)
        {
            try
            {
                var rps = await _rolePermissionService.GetAllPermissionByRoleId(id);

                return CustomResult("Data Load Successfully", rps);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetRolePermissionById/{id}")]
        public async Task<IActionResult> GetRolePermissionById(long id)
        {
            try
            {
                var rp = await _rolePermissionService.GetRolePermissionById(id);

                return CustomResult("Data Load Successfully", rp);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateRolePermission")]
        public async Task<IActionResult> CreateRolePermission(RolePermissionRequest rolePermissionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                RolePermissionResponse rp = await _rolePermissionService.CreateRolePermission(rolePermissionRequest);
                return CustomResult("Create Successful", rp, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpDelete("DeleteRolePermission/{id}")]
        public async Task<IActionResult> DeleteRolePermission(long id)
        {
            try
            {
                var rp = await _rolePermissionService.DeleteRolePermission(id);
                return CustomResult("Delete Role Successfull (Status)", rp, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }
    }
}
