using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface
{
    public interface IConversationService
    {
        Task<ConversationResponse> CreateConversation(ConversationRequest request);
        /*Task<ConversationResponse> UpdateConversationAsync(ConversationRequest request);*/
        Task<List<ConversationResponse>> GetConversation();
    }
}
