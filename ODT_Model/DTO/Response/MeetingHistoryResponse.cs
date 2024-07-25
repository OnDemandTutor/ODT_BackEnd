using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class MeetingHistoryResponse
    {
        public long Id { get; set; }
        
        public long StudentId { get; set; }

        public long MentorId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Cost { get; set; }

        public int Rating { get; set; }

        public string Status { get; set; }
    }
}
