using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class CreateStudentSubcriptionRequest
    {
        public long UserId { get; set; }

        public long SubcriptionId { get; set; }
    }
}
