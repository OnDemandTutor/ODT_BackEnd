using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface
{
    public interface IMessageReactionService
    {
        List<MessageReactionResponse> GetMessageReactionByConversationMessageId(long id);

        Task<bool> DeleteMessageReaction(long id);

        Task<MessageReactionResponse> CreateMessageReaction(MessageReactionRequest request);
    }
}
