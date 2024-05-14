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
        public long id { get; set; }

        public long userId { get; set; }

        [Required]
        public string academicLevel { get; set; }

        [Required]
        public string workPlace { get; set; }

        [Required]
        public string skill { get; set; }

        [Required]
        public BinaryReader video { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
