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
    public class ConversationMessageController : BaseController
    {
        private readonly IConversationMessageService _conversationMessage;
        public ConversationMessageController(IConversationMessageService conversationMessage)
        {
            this._conversationMessage = conversationMessage;
        }

        [HttpGet("GetConversationMessagesByConversationId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetConversationMessagesByConversationId(long id)
        {
            try
            {
                var conversationMessage = _conversationMessage.GetConversationMessagesByConversationId(id);

                return await Task.FromResult(CustomResult("Data loaded!", conversationMessage));
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }

        }

        [HttpDelete("DeleteConversationMessage/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteConversationMessage(long id)
        {
            try
            {
                var result = await _conversationMessage.DeleteConversationMessage(id);
                return CustomResult("Delete Successful.");
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateConversationMessage")]
        [Authorize]
        public async Task<IActionResult> CreateConversationMessage([FromForm] ConversationMessageRequest request)
        {
            /*if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }*/

            try
            {
                var response = await _conversationMessage.CreateConversationMessage(request);
                return CustomResult("Create Conversation message successful", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
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
