using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public long id { get; set; }

        public long userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
