using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Request
{
    public class RolePermissionRequest
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
    }
}
