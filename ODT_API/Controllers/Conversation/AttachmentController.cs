using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Service.Interface;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Conversation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : BaseController
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            this._attachmentService = attachmentService;
        }

        [HttpGet("GetImageAttachmentByConversationId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetImageAttachmentByConversationId(long id)
        {
            try
            {
                var attachment = _attachmentService.GetImageAttachmentByConversationId(id);

                return CustomResult("Data loaded!", attachment);
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

        [HttpGet("GetAnotherFileAttachmentByConversationId/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAnotherFileAttachmentByConversationId(long id)
        {
            try
            {
                var attachment = _attachmentService.GetAnotherFileAttachmentByConversationId(id);

                return CustomResult("Data loaded!", attachment);
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
