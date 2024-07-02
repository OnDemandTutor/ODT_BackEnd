using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class MessageReactionRequest
    {
        public long conversationMessageId { get; set; }
        public string reactionType { get; set; }
    }
}
