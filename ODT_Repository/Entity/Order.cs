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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long TransactionId { get; set; }

        public string PaymentCode { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public double Money { get; set; }

        public bool Status { get; set; }

        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
    }
}
