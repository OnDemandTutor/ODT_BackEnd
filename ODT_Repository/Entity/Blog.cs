using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public long id { get; set; }

        public long userId { get; set; }

        [Required]
        public string blogContent { get; set; }

        [Required]
        public string image { get; set; }

        public DateTime createDate { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
