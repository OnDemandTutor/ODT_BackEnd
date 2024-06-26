using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;
using System.Net;
using Tools;

namespace ODT_API.Controllers.BlogLike
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogLikeController : BaseController
    {
        private readonly IBlogLikeService _blogLikeService;
        public BlogLikeController(IBlogLikeService blogLikeService)
        {
            _blogLikeService = blogLikeService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBlogLikeById(long id)
        {
            var response = await _blogLikeService.GetBlogLikeByIdAsync(id);
            if (response == null)
            {
                return CustomResult("Data not found", System.Net.HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded", response);
        }

        [HttpGet("GetAllBlogLikeByBlogId/{id}")]
        public async Task<IActionResult> GetAllBlogLikeByBlogId(long id)
        {
            var response = await _blogLikeService.GetAllBlogLikeByBlogId(id);
            if (response == null)
            {
                return CustomResult("Data not found", System.Net.HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded", response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBlogLike([FromBody] BlogLikeRequest request)
        {
            try
            {
                var response = await _blogLikeService.CreateBlogLikeAsync(request);
                return CustomResult("Create successfully", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch
            {
                return CustomResult("Error!", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteBlogLikeAsync([FromBody] BlogLikeRequest request)
        {
            try
            {
                var response = await _blogLikeService.DeleteBlogLikeAsync(request);
                return CustomResult("Delete successfully", response);
            }
            catch
            {
                return CustomResult("Error!", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
