using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolePermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolePermissionResponse>> GetAllRolePermission(QueryObject queryObject)
        {
            var rps = _unitOfWork.RolePermissionRepository.Get(includeProperties: "Role,Permission",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!rps.Any())
            {
                throw new CustomException.DataNotFoundException("No RolePermission in Database");
            }

            var roleResponses = _mapper.Map<IEnumerable<RolePermissionResponse>>(rps);

            return roleResponses;
        }

        public async Task<List<RolePermissionResponse>> GetAllPermissionByRoleId(long id)
        {
            var rp = _unitOfWork.RolePermissionRepository.Get(filter: p =>
                                                        p.RoleId == id, includeProperties: "Role,Permission");

            if (rp == null)
            {
                throw new CustomException.DataNotFoundException($"RolePermission not found with RoleId: {id}");
            }

            var rolePermissionResponse = _mapper.Map<List<RolePermissionResponse>>(rp);
            return rolePermissionResponse;
        }

        public async Task<List<RolePermissionResponse>> GetRolePermissionById(long id)
        {
            var rp = _unitOfWork.RolePermissionRepository.Get(filter: p =>
                                                        p.Id == id, includeProperties: "Role,Permission");

            if (rp == null)
            {
                throw new CustomException.DataNotFoundException($"RolePermission not found with ID: {id}");
            }

            var rolePermissionResponse = _mapper.Map<List<RolePermissionResponse>>(rp);
            return rolePermissionResponse;
        }

        public async Task<RolePermissionResponse> CreateRolePermission(RolePermissionRequest rolePermissionRequest)
        {
            var role = _unitOfWork.RoleRepository.GetByID(rolePermissionRequest.RoleId);

            if (role == null)
            {
                throw new CustomException.DataNotFoundException($"Role not found with ID: {rolePermissionRequest.RoleId}");
            }

            var permission = _unitOfWork.PermissionRepository.GetByID(rolePermissionRequest.PermissionId);

            if (permission == null)
            {
                throw new CustomException.DataNotFoundException($"Permission not found with ID: {rolePermissionRequest.PermissionId}");
            }

            var existingRP = _unitOfWork.RolePermissionRepository.Get().FirstOrDefault(p => 
                                                                p.RoleId == rolePermissionRequest.RoleId &&
                                                                p.PermissionId == rolePermissionRequest.PermissionId);
                
            if (existingRP != null)
            {
                throw new CustomException.DataExistException($"RolePermission with Id '{rolePermissionRequest.PermissionId}' already exists.");
            }
            var rolePermissionResponse = _mapper.Map<RolePermissionResponse>(existingRP);
            var newRP = _mapper.Map<RolePermission>(rolePermissionRequest);

            _unitOfWork.RolePermissionRepository.Insert(newRP);
            _unitOfWork.Save();

            _mapper.Map(newRP, rolePermissionResponse);
            return rolePermissionResponse;
        }

        public async Task<bool> DeleteRolePermission(long id)
        {
            try
            {
                var rolePermission = _unitOfWork.RolePermissionRepository.GetByID(id);
                if (rolePermission == null)
                {
                    throw new CustomException.DataNotFoundException("RolePermission not found.");
                }

                _unitOfWork.RolePermissionRepository.Delete(rolePermission);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
