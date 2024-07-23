using CoreApiResponse;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Tools;

namespace ODT_API.Controllers.Mentor
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : BaseController
    {
        private readonly IMentorService _mentorService;

        public MentorController(IMentorService mentorService)
        { 
            _mentorService = mentorService;
        }

        [HttpGet("GetAllMentor")]
        public async Task<IActionResult> GetAllMentor([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var mentors = await _mentorService.GetAllMentor(queryPbject);
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

        [HttpGet("GetAllMentorVerify")]
        public IActionResult GetAllMentorVerify([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var mentors = _mentorService.GetAllMentorVerify(queryPbject);
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

        [HttpGet("GetAllMentorWaiting")]
        public IActionResult GetAllMentorWaiting([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var mentors = _mentorService.GetAllMentorWaiting(queryPbject);
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

        [HttpGet("GetMentorById/{id}")]
        public async Task<IActionResult> GetMentorById(long id)
        {
            try
            {
                var mentor = await _mentorService.GetMentorById(id);

                return CustomResult("Data Load Successfully", mentor);
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

        [HttpPatch("UpdateMentor/{id}")]
        public async Task<IActionResult> UpdateMentor(long id, [FromForm] MentorRequest mentorRequest)
        {
            try
            {
                MentorResponse mentor = await _mentorService.UpdateMentor(id, mentorRequest);
                return CustomResult("Update Sucessfully", mentor, HttpStatusCode.OK);
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

        [HttpPatch("UpdateOnlineStatus/{id}")]
        public async Task<IActionResult> UpdateOnlineStatus(long id, UpdateMentorOnlineStatusResquest updateMentorOnlineStatusResquest)
        {
            try
            {
                UpdateMentorOnlineStatusResponse onlineStatus = await _mentorService.UpdateOnlineStatus(id, updateMentorOnlineStatusResquest);
                return CustomResult("Update Successfully", onlineStatus, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, id, HttpStatusCode.NotFound);
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

        [HttpPatch("VerifyMentor/{id}")]
        public async Task<IActionResult> VerifyMentor(long id)
        {
            try
            {
                MentorResponse mentorResponse = await _mentorService.VerifyMentor(id);
                return CustomResult("Update Successfully", mentorResponse, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("UpdateMentorLoggingIn")]
        [Authorize]
        public async Task<IActionResult> UpdateMentorLoggingIn([FromBody] MentorRequest mentorRequest)
        {
            try
            {
                var mentor = _mentorService.UpdateMentorLoggingIn(mentorRequest);
                return CustomResult("Update mentor successful!!", mentor, HttpStatusCode.OK);
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
