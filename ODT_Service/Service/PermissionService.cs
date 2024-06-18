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
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionResponse>> GetAllPermission(QueryObject queryObject)
        {
            Expression<Func<Permission, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = p => p.PermissionName.Contains(queryObject.Search);
            }

            var permissions = _unitOfWork.PermissionRepository.Get(
                filter: filter,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!permissions.Any())
            {
                throw new CustomException.DataNotFoundException("No Permission in Database");
            }

            var permissionResponses = _mapper.Map<IEnumerable<PermissionResponse>>(permissions);

            return permissionResponses;
        }

        public async Task<PermissionResponse> GetPermissionById(long id)
        {
            var permission = _unitOfWork.PermissionRepository.GetByID(id);

            if (permission == null)
            {
                throw new CustomException.DataNotFoundException($"Permission not found with ID: {id}");
            }

            var permissionResponse = _mapper.Map<PermissionResponse>(permission);
            return permissionResponse;
        }

        public async Task<PermissionResponse> CreatePermission(PermissionRequest permissionRequest)
        {
            var existingPermission = _unitOfWork.PermissionRepository.Get().FirstOrDefault(p => p.PermissionName.ToLower() == permissionRequest.PermissionName.ToLower());

            if (existingPermission != null)
            {
                throw new CustomException.DataExistException($"Permission with name '{permissionRequest.PermissionName}' already exists.");
            }
            var permissionResponse = _mapper.Map<PermissionResponse>(existingPermission);
            var newPermission = _mapper.Map<Permission>(permissionRequest);

            _unitOfWork.PermissionRepository.Insert(newPermission);
            _unitOfWork.Save();

            _mapper.Map(newPermission, permissionResponse);
            return permissionResponse;
        }

        public async Task<PermissionResponse> UpdatePermission(long id, PermissionRequest permissionRequest)
        {
            var existingPermission = _unitOfWork.PermissionRepository.GetByID(id);

            if (existingPermission == null)
            {
                throw new CustomException.DataNotFoundException($"Permission with ID {id} not found.");
            }

            var duplicateExists = await _unitOfWork.PermissionRepository.ExistsAsync(p =>
                p.Id != id &&
                p.PermissionName.ToLower() == permissionRequest.PermissionName.ToLower()
            );

            if (duplicateExists)
            {
                throw new CustomException.DataExistException($"Permission with name '{permissionRequest.PermissionName}' already exists in Data.");
            }

            _mapper.Map(permissionRequest, existingPermission);
            _unitOfWork.Save();

            var permissionResponse = _mapper.Map<PermissionResponse>(existingPermission);
            return permissionResponse;
        }
        public async Task<bool> DeletePermission(long id)
        {
            try
            {
                var permission = _unitOfWork.PermissionRepository.GetByID(id);
                if (permission == null)
                {
                    throw new CustomException.DataNotFoundException("Permission not found.");
                }

                _unitOfWork.PermissionRepository.Delete(permission);
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
