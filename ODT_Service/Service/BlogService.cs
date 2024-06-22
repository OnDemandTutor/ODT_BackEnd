using AutoMapper;
using Microsoft.AspNetCore.Http;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;
        private readonly IHttpContextAccessor _contextAccessor;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper,
                            Tools.Firebase firebase,
                            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
            _contextAccessor = contextAccessor;
        }

        public async Task<BlogResponse> CreateBlog(BlogRequest request)
        {
            string imageUrl = null;
            var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

            if (!long.TryParse(getUserId, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            if (request.FormFile != null)
            {
                if (request.FormFile.Length > 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                imageUrl = await _firebase.UploadImageAsync(request.FormFile);
            }

            var respone = _mapper.Map<Blog>(request);
            respone.UserId = long.Parse(getUserId);
            respone.TotalLike = 0;
            respone.Image = imageUrl;
            respone.CreateDate = DateTime.UtcNow;

            await _unitOfWork.BlogRepository.AddAsync(respone);
            return _mapper.Map<BlogResponse>(respone);
        }


        public async Task<bool> DeleteBlog(long id)
        {
            var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

            if (!long.TryParse(getUserId, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var getOneBlog = _unitOfWork.BlogRepository
                                .Get(filter: x =>
                                    x.UserId == userId && x.Id == id)
                                .FirstOrDefault();
            if (getOneBlog != null)
            {
                _unitOfWork.BlogRepository.Delete(getOneBlog.Id);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BlogResponse>> GetAllBlog()
        {
            var listBlog = _unitOfWork.BlogRepository.Get(includeProperties: "User");
            return _mapper.Map<IEnumerable<BlogResponse>>(listBlog);
        }

        public async Task<BlogResponse> GetOneBlog(long id)
        {
            var getOneBlog = _unitOfWork.BlogRepository
                                .Get(filter: x => x.Id == id, includeProperties: "User")
                                .FirstOrDefault();

            if (getOneBlog == null)
            {
                return null;
            }
            var respone = _mapper.Map<BlogResponse>(getOneBlog);
            return respone;
        }

        public async Task<BlogResponse> UpdateBlog(long id, BlogRequest request)
        {
            var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

            if (!long.TryParse(getUserId, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var existBlog = _unitOfWork.BlogRepository
                                .Get(x => x.Id == id
                                        && x.User.Id == long.Parse(getUserId))
                                .FirstOrDefault();
            if (existBlog == null)
            {
                return null;
            }

            string imageUrl = null;
            if (request.FormFile != null)
            {
                if (request.FormFile.Length > 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                imageUrl = await _firebase.UploadImageAsync(request.FormFile);
            }

            var response = _mapper.Map(request, existBlog);
            response.Image = imageUrl;

            await _unitOfWork.BlogRepository.UpdateAsync(response);
            return _mapper.Map<BlogResponse>(response);
        }
    }
}
