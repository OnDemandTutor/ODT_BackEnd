using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Conversation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : BaseController
    {
        private readonly IConversationService _conversationService;
        public ConversationController(IConversationService conversationService)
        {
            this._conversationService = conversationService;
        }
        [HttpPost("CreateConversation")]
        [Authorize]
        public async Task<IActionResult> CreateConversation([FromBody] ConversationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }
            try
            {
                var response = await _conversationService.CreateConversation(request);
                return CustomResult("Create Conversation successful", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("GetConversationByUserId1")]
        [Authorize]
        public async Task<IActionResult> GetConversation()
        {
            try
            {
                var conversation = await _conversationService.GetConversation();

                return CustomResult("Conversation.", conversation);
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
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

    }
}
