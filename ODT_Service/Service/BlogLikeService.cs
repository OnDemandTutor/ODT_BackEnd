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
using System.Transactions;
using Tools;

namespace ODT_Service.Service
{
    public class BlogLikeService : IBlogLikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public BlogLikeService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<BlogLikeResponse> CreateBlogLikeAsync(BlogLikeRequest request)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

                if (!long.TryParse(getUserId, out long userId))
                {
                    throw new Exception("User ID claim invalid.");
                }
                var checkBlogLike = _unitOfWork.BlogLikeRepository
                                    .Get(filter: x => x.Blog.Id == request.BlogId
                                        && x.User.Id == userId, includeProperties: "Blog")
                                    .FirstOrDefault();

                if (checkBlogLike != null)
                {
                    throw new CustomException.InvalidDataException("Invalid data");
                }

                var blogLike = _mapper.Map(request, checkBlogLike);
                blogLike.Status = true;
                blogLike.UserId = long.Parse(getUserId);
                _unitOfWork.BlogLikeRepository.Insert(blogLike);

                var getBlog = _unitOfWork.BlogRepository.GetByID(request.BlogId);
                getBlog.TotalLike += 1;

                _unitOfWork.Save();

                transaction.Complete();
                return _mapper.Map<BlogLikeResponse>(blogLike);
            }
        }

        public async Task<BlogLikeResponse> GetBlogLikeByIdAsync(long id)
        {
            var getById = await _unitOfWork.BlogLikeRepository.GetByIdAsync(id);
            return _mapper.Map<BlogLikeResponse>(getById);
        }

        public async Task<BlogLikeResponse> DeleteBlogLikeAsync(BlogLikeRequest request)
        {
            var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

            if (!long.TryParse(getUserId, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }
            var checkBlogLike = _unitOfWork.BlogLikeRepository
                                    .Get(filter: x => x.Blog.Id == request.BlogId
                                        && x.User.Id == userId, includeProperties: "Blog")
                                    .FirstOrDefault();

            if (checkBlogLike == null)
            {
                throw new CustomException.InvalidDataException("Invalid data");
            }

            checkBlogLike.Blog.TotalLike -= 1;
            var blogLike = _mapper.Map<BlogLike>(checkBlogLike);
            _unitOfWork.BlogLikeRepository.Delete(blogLike);

            _unitOfWork.Save();

            return _mapper.Map<BlogLikeResponse>(blogLike);
        }

        public async Task<IEnumerable<BlogLikeResponse>> GetAllBlogLikeByBlogId(long id)
        {
            var getById = _unitOfWork.BlogLikeRepository.Get(filter: x => x.BlogId == id && x.Status);
            return _mapper.Map<IEnumerable<BlogLikeResponse>>(getById);
        }
    }
}
