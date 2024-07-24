using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class MentorMajorResponse
    {
        public long Id { get; set; }

        public long MentorId { get; set; }

        public long MajorId { get; set; }

        public Mentor Mentor { get; set; }

        public Major Major { get; set; }
    }
}
