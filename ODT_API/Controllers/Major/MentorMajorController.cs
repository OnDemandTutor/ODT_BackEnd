using CoreApiResponse;
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
    public class MentorMajorController : BaseController
    {
           private readonly IMentorMajorService _mentorMajorService;

        public MentorMajorController(IMentorMajorService mentorMajorService)
        {
            _mentorMajorService = mentorMajorService;
        }

        [HttpGet("GetAllMentorMajor")]
        public IActionResult GetAllMentorMajor([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var mms = _mentorMajorService.GetAllMentorMajor(queryPbject);
                return CustomResult("Data Load Successfully", mms);
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

        [HttpGet("GetAllMajorByMentorId/{id}")]
        public async Task<IActionResult> GetAllMajorByMentorId(long id)
        {
            try
            {
                var majors = await _mentorMajorService.GetAllMajorByMentorId(id);

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

        [HttpGet("GetAllMentorByMajorId/{id}")]
        public async Task<IActionResult> GetAllMentorByMajorId(long id)
        {
            try
            {
                var mentors = await _mentorMajorService.GetAllMentorByMajorId(id);

                return CustomResult("Data Load Successfully", mentors);
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

        [HttpGet("GetMentorMajorById/{id}")]
        public async Task<IActionResult> GetMentorMajorById(long id)
        {
            try
            {
                var mm = await _mentorMajorService.GetMentorMajorById(id);

                return CustomResult("Data Load Successfully", mm);
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

        [HttpPost("CreateMentorMajor")]
        public async Task<IActionResult> CreateMentorMajor(MentorMajorRequest mentorMajorRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                MentorMajorResponse mm = await _mentorMajorService.CreateMentorMajor(mentorMajorRequest);
                return CustomResult("Create Successful", mm, HttpStatusCode.OK);
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

        [HttpDelete("DeleteMentorMajor/{id}")]
        public async Task<IActionResult> DeleteMentorMajor(long id)
        {
            try
            {
                var mm = await _mentorMajorService.DeleteMentorMajor(id);
                return CustomResult("Delete Role Successfull (Status)", mm, HttpStatusCode.OK);
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
