using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuStudy_Model.DTO.Request
{
    public class BlogCommentRequest
    {
        public long BlogId { get; set; }

        public string? Comment { get; set; }

    }
}
