using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Mentor")]
    public class Mentor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        
        public string AcademicLevel { get; set; }

        public string WorkPlace { get; set; }

        public string Status { get; set; }

        public string Skill { get; set; }

        public string Video { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
    }
}
