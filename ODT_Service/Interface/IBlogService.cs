using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Service.Interface
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllBlog();
        Task<BlogResponse> CreateBlog(BlogRequest request);
        Task<bool> DeleteBlog(long id);
        Task<BlogResponse> GetOneBlog(long id);
        Task<BlogResponse> UpdateBlog(long id, BlogRequest request);
    }
}
