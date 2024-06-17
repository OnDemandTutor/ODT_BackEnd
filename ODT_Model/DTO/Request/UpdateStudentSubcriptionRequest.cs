using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class UpdateStudentSubcriptionRequest
    {
        //[Required(ErrorMessage = "StudentID is required and it long!")]
        //public long StudentId { get; set; }
        [Required(ErrorMessage = "SubcriptionID is required and it long!")]
        public long SubcriptionId { get; set; }
        [Required(ErrorMessage = "CurrentMeeting is required and it int!")]
        public int CurrentMeeting { get; set; }
        [Required(ErrorMessage = "CurrentQuestion is required and it int!")]
        public int CurrentQuestion { get; set; }

        //[ForeignKey("StudentId")]
        //public virtual Student Student { get; set; }

        //[ForeignKey("SubcriptionId")]
        //public virtual Subcription Subcription { get; set; }
    }
}
