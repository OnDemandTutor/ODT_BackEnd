using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("QuestionComment")]
    public class QuestionComment
    {
        [Key]
        public long id { get; set; }

        public long userId { get; set; }

        public long questionId { get; set; }

        [Required]
        public string content { get; set; }

        public DateTime createDate { get; set; }

        public DateTime modifiedDate { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        [ForeignKey("questionId")]
        public Question question { get; set; }

    }
}
