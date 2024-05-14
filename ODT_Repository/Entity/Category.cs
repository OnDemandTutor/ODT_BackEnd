using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string categoryName { get; set; }
    }
}
