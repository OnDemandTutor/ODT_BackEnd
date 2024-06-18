using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class TransactionRequest
    {
        [Required]
        public long WalletId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public double Ammount { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
