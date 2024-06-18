using CoreApiResponse;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("GetAllPermission")]
        public IActionResult GetAllPermission([FromQuery] QueryObject queryObject)
        {
            try
            {
                var rps = _permissionService.GetAllPermission(queryObject);
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

        [HttpGet("GetPermissionById/{id}")]
        public async Task<IActionResult> GetPermissionById(long id)
        {
            try
            {
                var rp = await _permissionService.GetPermissionById(id);

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

        [HttpPost("CreatePermission")]
        public async Task<IActionResult> CreatePermission(PermissionRequest permissionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                PermissionResponse rp = await _permissionService.CreatePermission(permissionRequest);
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

        [HttpPatch("UpdatePermission/{id}")]
        public async Task<IActionResult> UpdatePermission(long id, PermissionRequest PermissionRequest)
        {
            try
            {
                PermissionResponse rp = await _permissionService.UpdatePermission(id, PermissionRequest);
                return CustomResult("Update Sucessfully", rp, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.DataExistException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeletePermission/{id}")]
        public async Task<IActionResult> DeleteRolePermission(long id)
        {
            try
            {
                var rp = await _permissionService.DeletePermission(id);
                return CustomResult("Delete Permission Successfull (Status)", rp, HttpStatusCode.OK);
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
