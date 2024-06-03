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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long RoleId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public string IdentityCard { get; set; }

        public string Gender { get; set; }

        public string Avatar { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Status { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
