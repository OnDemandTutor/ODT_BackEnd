using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class MessageReactionResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ConversationMessageId { get; set; }
        public string ReactionType { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
