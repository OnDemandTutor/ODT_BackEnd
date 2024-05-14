using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public long id { get; set; }

        public long subcriptionId { get; set; }

        public long studentId { get; set; }

        [Required]
        public string paymentCode { get; set; }

        [Required]
        public string description { get; set; }

        public DateTime createDate { get; set; }

        [Required]
        public double money { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("subcriptionId")]
        public Subcription subcription { get; set; }

        [ForeignKey("studentId")]
        public Student student { get; set; }
    }
}
