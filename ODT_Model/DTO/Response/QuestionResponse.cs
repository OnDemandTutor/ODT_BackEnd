using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class QuestionResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string CategoryName { get; set; }
        public long StudentId { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }
        
        public int TotalRating { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
        
    }
}
