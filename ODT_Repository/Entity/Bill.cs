using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public long id { get; set; }

        public long orderId { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("orderId")]
        public Order order { get; set; }
    }
}
