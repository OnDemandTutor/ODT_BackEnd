using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Wallet")]
    public class Wallet
    {
        [Key]
        public long id { get; set; }

        public long userId { get; set; }

        [Required]
        public double balance { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
