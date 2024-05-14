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
        public long id { get; set; }

        [Required]
        public string subcriptionName { get; set; }

        [Required]
        public double subcriptionPrice { get; set; }

        [Required]
        public bool status { get; set; }
    }
}
