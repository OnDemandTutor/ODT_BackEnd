using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Subcription")]
    public class Subcription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string SubcriptionName { get; set; }

        public double SubcriptionPrice { get; set; }

        public int LimitQuestion { get; set; }

        public int LimitMeeting { get; set; }

        public bool Status { get; set; }
    }
}
