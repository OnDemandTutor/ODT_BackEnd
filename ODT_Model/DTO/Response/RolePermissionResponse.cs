using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Model.DTO.Response
{
    public class RolePermissionResponse
    {
        public long Id { get; set; }

        public Role Role { get; set; }

        public Permission Permission { get; set; }

    }
}
