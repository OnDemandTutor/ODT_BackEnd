using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [AllowNull]
        public string AcademicLevel { get; set; }
        [AllowNull]
        public string WorkPlace { get; set; }

        public string OnlineStatus { get; set; }
        [AllowNull]
        public string Skill { get; set; }
        [AllowNull]
        public string Video { get; set; }

        public bool VerifyStatus { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
