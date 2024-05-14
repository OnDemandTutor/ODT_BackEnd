using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("RolePermission")]
    public class RolePermission
    {
        public long roleId { get; set; }

        public long permissionId { get; set; }

        [ForeignKey("roleId")]
        public Role role { get; set; }

        [ForeignKey("permissionId")]
        public Permission permission { get; set; }

    }
}
