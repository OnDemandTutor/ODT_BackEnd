using AutoMapper;
using FuStudy_Model.DTO.Request;
using FuStudy_Model.DTO.Response;
using Microsoft.AspNetCore.Http;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Tools;

namespace ODT_Service.Service
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;
        private readonly IHttpContextAccessor _contextAccessor;

        public BlogCommentService(IUnitOfWork unitOfWork, IMapper mapper,
                                    Tools.Firebase firebase,
                                    IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
            _contextAccessor = contextAccessor;
        }

        public async Task<BlogCommentResponse> CreateBlogComment(BlogCommentRequest request)
        {
            try
            {
                var checkBlog = _unitOfWork.BlogRepository.GetByID(request.BlogId);

                if (checkBlog == null)
                {
                    throw new CustomException.DataNotFoundException("Bad request! Blog doest not exist !");
                }

                var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

                if (!long.TryParse(getUserId, out long userId))
                {
                    throw new Exception("User ID claim invalid.");
                }

                var getInfoUser = _unitOfWork.UserRepository
                                                .Get(filter: x => x.Id == long.Parse(getUserId))
                                                .FirstOrDefault();

                var blogCmt = _mapper.Map<BlogComment>(request);
                blogCmt.UserId = long.Parse(getUserId);
                blogCmt.CreateDate = DateTime.Now;
                blogCmt.ModifiedDate = blogCmt.CreateDate;
                blogCmt.Status = true;

                await _unitOfWork.BlogCommentRepository.AddAsync(blogCmt);
                var response = _mapper.Map<BlogCommentResponse>(blogCmt);
                response.Avatar = getInfoUser.Avatar;
                response.Fullname = getInfoUser.Fullname;
                
                return response;
            }
            catch
            {
                throw new CustomException.DataNotFoundException("Error! Have some thing wrong at BlogComment !");
            }
        }

        public async Task<bool> DeleteBlogComment(long id)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

                    if (!long.TryParse(getUserId, out long userId))
                    {
                        throw new Exception("User ID claim invalid.");
                    }

                    var getBlogComment = _unitOfWork.BlogCommentRepository
                                                        .Get(filter: x => x.Id == id
                                                                    && x.UserId == long.Parse(getUserId))
                                                        .FirstOrDefault();
                    var getCommentImgExist = _unitOfWork.CommentImageRepository
                                             .Get(filter: x => x.BlogCommentId == id)
                                             .FirstOrDefault();

                    if (getCommentImgExist != null)
                    {
                        getCommentImgExist.Status = false;
                        _unitOfWork.CommentImageRepository.Update(getCommentImgExist);
                    }

                    getBlogComment.Status = false;
                    _unitOfWork.BlogCommentRepository.Update(getBlogComment);

                    _unitOfWork.Save();
                    transaction.Complete();
                    return true;
                }
            }
            catch
            {
                throw new CustomException.InvalidDataException("Invalid data! Please check.");
            }
        }

        public async Task<IEnumerable<BlogCommentResponse>> GetAllCommentABlog(QueryObject queryObject)
        {
            try
            {
                Expression<Func<Blog, bool>> filter = null;
                if (!string.IsNullOrWhiteSpace(queryObject.Search))
                {
                    filter = blogCmt => blogCmt.Id.ToString().Contains(queryObject.Search);
                }

                var checkBlog = _unitOfWork.BlogRepository.Get(
                                                filter: filter,
                                                pageIndex: queryObject.PageIndex,
                                                pageSize: queryObject.PageSize)
                                                        .FirstOrDefault();
                if (checkBlog == null)
                {
                    throw new CustomException.InvalidDataException("Bad request!");
                }
                var commentContent = _unitOfWork.BlogCommentRepository
                                .Get(filter: p => p.BlogId == checkBlog.Id && p.Status == true,
                                includeProperties: "User");

                var response = _mapper.Map<IEnumerable<BlogCommentResponse>>(commentContent);
                return response;
            }
            catch (Exception ex)
            {
                throw new CustomException.InvalidDataException(ex.Message, "Bad Request!");
            }
        }
        public async Task<BlogCommentResponse> UpdateBlogComment(BlogCommentRequest request, long id)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            };

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                var getUserId = Authentication.GetUserIdFromHttpContext(_contextAccessor.HttpContext);

                if (!long.TryParse(getUserId, out long userId))
                {
                    throw new Exception("User ID claim invalid.");
                }

                var getInfoUser = _unitOfWork.UserRepository
                                                .Get(filter: x => x.Id == long.Parse(getUserId))
                                                .FirstOrDefault();

                var getBlogComment = _unitOfWork.BlogCommentRepository
                                                    .Get(filter: x => x.Id == id
                                                            && x.UserId == userId
                                                            && x.BlogId == request.BlogId
                                                            && x.Status)
                                                    .FirstOrDefault();
                var getCommentImgExist = _unitOfWork.CommentImageRepository
                                         .Get(filter: x => x.BlogCommentId == id
                                                    && x.Status)
                                         .FirstOrDefault();

                getBlogComment.Comment = request.Comment;
                getBlogComment.ModifiedDate = DateTime.Now;
                _unitOfWork.BlogCommentRepository.Update(getBlogComment);

                _unitOfWork.Save();

                transaction.Complete();

                var response = _mapper.Map<BlogCommentResponse>(getBlogComment);
                response.Fullname = getInfoUser.Fullname;
                response.Avatar = getInfoUser.Avatar;
                return response;
            }
        }

    }
}
