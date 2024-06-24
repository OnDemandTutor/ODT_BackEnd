using FuStudy_Model.DTO.Request;
using FuStudy_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IBlogCommentService
    {
        Task<IEnumerable<BlogCommentResponse>> GetAllCommentABlog(QueryObject queryObject);
        Task<BlogCommentResponse> CreateBlogComment(BlogCommentRequest request);
        Task<BlogCommentResponse> UpdateBlogComment(BlogCommentRequest request, long id);
        Task<bool> DeleteBlogComment(long id);
    }
}
