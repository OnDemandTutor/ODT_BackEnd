using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface
{
    public interface IAttachmentService
    {
        List<AttachmentResponse> GetImageAttachmentByConversationId(long conversationId);

        List<AttachmentResponse> GetAnotherFileAttachmentByConversationId(long conversationId);
    }
}
