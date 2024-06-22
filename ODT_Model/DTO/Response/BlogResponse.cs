using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class BlogResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string BlogContent { get; set; }
        public string Image { get; set; }
        public int TotalLike { get; set; }
        public DateTime CreateDate { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
    }
}
