using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;

namespace ODT_API.Controllers.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlog()
        {
            var response = await _blogService.GetAllBlog();
            if (response == null)
            {
                return CustomResult("Data not found", System.Net.HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded", response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBlog(long id)
        {
            var response = await _blogService.GetOneBlog(id);
            if (response == null)
            {
                return CustomResult("Data not found", System.Net.HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded", response);
        }

        [HttpPost("/CreateBlog")]
        [Authorize]
        public async Task<IActionResult> CreateBlog([FromForm] BlogRequest request)
        {
            var reponse = await _blogService.CreateBlog(request);
            if (reponse == null)
            {
                return CustomResult("Create is not success !", System.Net.HttpStatusCode.BadRequest);
            }
            return CustomResult("Create successfully", reponse);
        }

        [HttpPost("/UpdateBlog/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlog(long id, [FromForm] BlogRequest request)
        {
            var reponse = await _blogService.UpdateBlog(id, request);
            if (reponse == null)
            {
                return CustomResult("Update is not success !", System.Net.HttpStatusCode.BadRequest);
            }
            return CustomResult("Update successfully", reponse);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOneBlog(long id)
        {
            var reponse = await _blogService.DeleteBlog(id);
            if (!reponse)
            {
                return CustomResult("Delete is not success !", System.Net.HttpStatusCode.BadRequest);
            }
            return CustomResult("Delete successfully", reponse);
        }
    }
}
