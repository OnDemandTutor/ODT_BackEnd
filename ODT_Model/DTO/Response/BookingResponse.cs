using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class BookingResponse
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long MentorId { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime EndTime { get; set; }

        public String Warning { get; set; }

        public string Status { get; set; }

        public virtual User User { get; set; }
        
        public virtual Mentor Mentor { get; set; }
    }
}
