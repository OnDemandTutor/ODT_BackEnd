using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface
{
    public interface IConversationMessageService
    {
        Task<ConversationMessageResponse> CreateConversationMessage(ConversationMessageRequest request);
        List<ConversationMessageResponse> GetConversationMessagesByConversationId(long conversationId);
        Task<bool> DeleteConversationMessage(long id);
    }
}
