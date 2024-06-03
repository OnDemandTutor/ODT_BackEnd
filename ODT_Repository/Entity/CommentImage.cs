using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("CommentImage")]
    public class CommentImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long BlogCommentId { get; set; }

        public string Image { get; set; }

        public bool Status { get; set; }

        [ForeignKey("BlogCommentId")]
        public virtual BlogComment BlogComment { get; set; }
    }
}
