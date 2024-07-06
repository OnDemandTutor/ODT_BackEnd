using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class ConversationResponse
    {
        public long Id { get; set; }
        public long User1Id { get; set; }
        public long User2Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EndTime { get; set; }
        public string LastMessage { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsClose { get; set; }
    }
}
