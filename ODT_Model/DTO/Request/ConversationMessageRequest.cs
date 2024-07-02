using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class ConversationMessageRequest
    {
        public long ConversationId { get; set; }
        public string content { get; set; }

        public IFormFile? File { get; set; } = null;
    }
}
