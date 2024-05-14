using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("StudentSubcription")]
    public class StudentSubcription
    {
        public long studentId { get; set; }

        public long subcriptionId { get; set; }

        [Required]
        public int limitQuestion { get; set; }

        [Required]
        public int currentQuestion { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        [ForeignKey("studentId")]
        public Student student { get; set; }

        [ForeignKey("subcriptionId")]
        public Subcription subcription { get; set; }
    }
}
