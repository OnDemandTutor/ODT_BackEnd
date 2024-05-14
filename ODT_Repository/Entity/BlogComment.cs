using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("BlogComment")]
    public class BlogComment
    {
        [Key]
        public long id { get; set; }

        public long userId { get; set; }

        public long blogId { get; set; }

        [Required]
        public string blogComment { get; set; }

        public DateTime createDate { get; set; }

        public DateTime modifiedDate { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        [ForeignKey("blogId")]
        public Blog blog { get; set; }
    }
}
