using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class StudentSubcriptionResponse
    {
        public long Id { get; set; }

        public long StudentId { get; set; }

        public long SubcriptionId { get; set; }

        public int CurrentMeeting { get; set; }

        public int CurrentQuestion { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Status {  get; set; }

        public virtual Student Student { get; set; }

        public virtual Subcription Subcription { get; set; }
    }
}
