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
    public class MessageReactionController : BaseController
    {
        private readonly IMessageReactionService _messageReactionService;
        public MessageReactionController(IMessageReactionService messageReactionService)
        {
            this._messageReactionService = messageReactionService;
        }

        [HttpGet("GetMessageReactionByConServationMessageId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetMessageReactionByConversationMessageId(long id)
        {
            try
            {
                var messageReactions =  _messageReactionService.GetMessageReactionByConversationMessageId(id);

                return CustomResult("Data loaded!", messageReactions);
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
        [HttpDelete("DeleteMessageReaction/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessageReaction(long id)
        {
            try
            {
                var result = await _messageReactionService.DeleteMessageReaction(id);
                if (result)
                {
                    return CustomResult("Delete Successful.");
                }
                else
                {
                    return CustomResult("Delete Failed.", HttpStatusCode.InternalServerError);
                }
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


        [HttpPost("CreateMessageReaction")]
        [Authorize]
        public async Task<IActionResult> CreateMessageReaction([FromBody] MessageReactionRequest request)
        {
            /*if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }*/

            try
            {
                var response = await _messageReactionService.CreateMessageReaction(request);
                return CustomResult("Create message reaction successful", response);
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
