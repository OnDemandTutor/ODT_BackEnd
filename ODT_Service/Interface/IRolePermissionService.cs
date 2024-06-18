using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermissionResponse>> GetAllRolePermission(QueryObject queryObject);
        Task<List<RolePermissionResponse>> GetAllPermissionByRoleId(long id);
        Task<List<RolePermissionResponse>> GetRolePermissionById(long id);
        Task<RolePermissionResponse> CreateRolePermission(RolePermissionRequest rolePermissionRequest);
        Task<bool> DeleteRolePermission(long id);
    }
}
