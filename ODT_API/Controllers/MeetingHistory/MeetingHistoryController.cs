using CoreApiResponse;
using ODT_Service.Interface;
using ODT_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace ODT_API.Controllers.MeetingHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingHistoryController : BaseController
    {
        private readonly IMeetingHistory _meetingHistory;

        public MeetingHistoryController(IMeetingHistory meetingHistory)
        {
            _meetingHistory = meetingHistory;
        }

        [HttpGet("GetAllMeetingHistory")]
        public async Task<IActionResult> GetAllMeetingHistory([FromQuery] QueryObject queryPbject)
        {
            try
            {
                var meetings = await _meetingHistory.GetAllMeetingHistory(queryPbject);
                return CustomResult("Data Load Successfully", meetings);
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

        [HttpGet("GetAllMeetingHistoryByStudentId/{id}")]
        public async Task<IActionResult> GetAllMeetingHistoryByStudentId(long id)
        {
            try
            {
                var meeting = await _meetingHistory.GetAllMeetingHistoryByStudentId(id);

                return CustomResult("Data Load Successfully", meeting);
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

        [HttpGet("GetMeetingHistoryByMentorId/{id}")]
        public async Task<IActionResult> GetMeetingHistoryByMentorId(long id)
        {
            try
            {
                var meeting = await _meetingHistory.GetMeetingHistoryByMentorId(id);

                return CustomResult("Data Load Successfully", meeting);
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

        [HttpGet("GetMeetingHistoryById/{id}")]
        public async Task<IActionResult> GetMeetingHistoryById(long id)
        {
            try
            {
                var meeting = await _meetingHistory.GetMeetingHistoryById(id);

                return CustomResult("Data Load Successfully", meeting);
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
