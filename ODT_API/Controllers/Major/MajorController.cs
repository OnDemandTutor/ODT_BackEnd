using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Service.Interface;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Major
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : BaseController
    {
        private readonly IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        [HttpGet("GetAllMajor")]
        public IActionResult GetAllMajor([FromQuery] QueryObject queryObject)
        {
            try
            {
                var majors = _majorService.GetAllMajor(queryObject);
                return CustomResult("Data Load Successfully", majors);
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

        [HttpGet("GetMajorById/{id}")]
        public async Task<IActionResult> GetMajorById(long id)
        {
            try
            {
                var major = await _majorService.GetMajorById(id);

                return CustomResult("Data Load Successfully", major);
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

        [HttpPost("CreateMajor")]
        public async Task<IActionResult> CreateMajor(MajorRequest majorRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                MajorResponse major = await _majorService.CreateMajor(majorRequest);
                return CustomResult("Create Successful", major, HttpStatusCode.OK);
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

        [HttpPatch("UpdateMajor/{id}")]
        public async Task<IActionResult> UpdateMajor(long id, MajorRequest majorRequest)
        {
            try
            {
                MajorResponse major = await _majorService.UpdateMajor(id, majorRequest);
                return CustomResult("Update Sucessfully", major, HttpStatusCode.OK);
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

        [HttpDelete("DeleteMajor/{id}")]
        public async Task<IActionResult> DeleteMajor(long id)
        {
            try
            {
                var major = await _majorService.DeleteMajor(id);
                return CustomResult("Delete Major Successfull (Status)", major, HttpStatusCode.OK);
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
