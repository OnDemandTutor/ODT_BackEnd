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
        public long Id { get; set; }

        public long UserId { get; set; }

        public string BlogContent { get; set; }

        public string Image { get; set; }

        public int TotalLike { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
