using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    // ConversationMessageResponse.cs

        public class ConversationMessageResponse
        {
            public long Id { get; set; }

            public long SenderId { get; set; }

            public DateTime CreateTime { get; set; }

            public string Content { get; set; }

            public bool IsDelete { get; set; }

            public DateTime DeleteAt { get; set; }

            public bool IsSeen { get; set; }

            public List<AttachmentResponse> Attachments { get; set; }
            public List<MessageReactionResponse> MessageReactions { get; set; }


    }

}
