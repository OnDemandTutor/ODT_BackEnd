using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class WalletResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public double Balance { get; set; }
        public bool Status { get; set; }
    }
}