using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class TransactionResponse
    {
        public long Id { get; set; }
        public long WalletId { get; set; }
        public string Type { get; set; }
        public double Ammount { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }

        // Thêm các thuộc tính liên quan đến User
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Fullname { get; set; }
    }
}

