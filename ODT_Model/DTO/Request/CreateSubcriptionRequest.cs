using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuStudy_Model.DTO.Request
{
    public class CreateSubcriptionRequest
    {
        [StringLength(maximumLength: 40, MinimumLength = 8)]
        public required string SubcriptionName { get; set; }
        [Required(ErrorMessage = "SubcriptionPrice is required")]
        public  double SubcriptionPrice { get; set; }
        [Required(ErrorMessage = "Limit Question in a number and required")]
        public int limitQuestion { get; set; }
        [Required(ErrorMessage = "Limit Meeting in a number and required")]
        public int limitMeeting { get; set; }
    }
}
