using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public string PaymentCode { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public double Money { get; set; }
        public bool Status { get; set; }
    }
}
