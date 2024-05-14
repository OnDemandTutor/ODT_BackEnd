using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string roleName { get; set; }
    }
}
