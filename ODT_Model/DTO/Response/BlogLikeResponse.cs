using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class BlogLikeResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BlogId { get; set; }
        public bool Status { get; set; }
    }
}
