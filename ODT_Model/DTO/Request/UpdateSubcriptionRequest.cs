using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class UpdateSubcriptionRequest
    {
        [StringLength(maximumLength: 40, MinimumLength = 8)]
        public required string SubcriptionName { get; set; }
        [Required(ErrorMessage = "SubcriptionPrice is required")]
        public double SubcriptionPrice { get; set; }
        [Required(ErrorMessage = "limit Question is a number and it required")]
        public int limitQuestion { get; set; }
        [Required(ErrorMessage = "limit Metting is a number and it required")]
        public int limitMetting { get; set; }
        [Required(ErrorMessage = "Status is required and it bool")]
        public bool Status { get; set; }
    }
}
