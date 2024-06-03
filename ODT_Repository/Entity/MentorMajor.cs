using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("MentorMajor")]
    public class MentorMajor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long MentorId { get; set; }

        public long MajorId { get; set; }

        [ForeignKey("MentorId")]
        public virtual Mentor Mentor { get; set; }

        [ForeignKey("MajorId")]
        public virtual Major Major { get; set; }
    }
}
