using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class BlogLikeRequest
    {
        [Required]
        public long BlogId { get; set; }
    }
}
