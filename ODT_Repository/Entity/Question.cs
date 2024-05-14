using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public long id { get; set; }

        public long studentId { get; set; }

        public long categoryId { get; set; }

        [Required]
        public string questionContent { get; set; }

        public DateTime createDate { get; set; }

        public DateTime modifiedDate { get; set; }

        [Required]
        public string image { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("studentId")]
        public Student student { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }

    }
}
