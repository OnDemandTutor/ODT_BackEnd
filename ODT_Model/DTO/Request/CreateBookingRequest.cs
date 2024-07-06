using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class CreateBookingRequest
    {

        public long MentorId { get; set; }

        [Required(ErrorMessage = "You must choose BookingMethod")]
        public string BookingMethod { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
