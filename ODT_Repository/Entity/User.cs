using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        public long id { get; set; }

        public long roleId { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string passWord { get; set; }

        [Required]
        public string fullName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string identityCard { get; set; }

        [Required]
        public DateOnly dob { get; set; }

        [Required]
        public string phone { get; set; }

        public DateTime createDate { get; set; }

        [Required]
        public bool status { get; set; }

        [ForeignKey("roleId")]
        public Role role { get; set; }
    }
}
