using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuStudy_Model.DTO.Response
{
    public class BlogCommentResponse
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long BlogId { get; set; }

        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Fullname { get; set; }
        public string Avatar { get; set; }

    }
}
