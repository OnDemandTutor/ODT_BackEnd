using CoreApiResponse;
using FuStudy_Model.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.BlogComment
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : BaseController
    {
        private readonly IBlogCommentService _blogCommentService;
        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        [HttpGet("GetAllCommentABlog")]
        public async Task<IActionResult> GetAllCommentABlog([FromQuery] QueryObject queryObject)
        {
            try
            {
                var response = await _blogCommentService.GetAllCommentABlog(queryObject);
                if (response == null)
                {
                    return CustomResult("Data not loaded", response);
                }
                return CustomResult("Data loaded", response);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateBlogComment")]
        [Authorize]
        public async Task<IActionResult> CreateBlogComment([FromForm] BlogCommentRequest request)
        {
            try
            {

                var response = await _blogCommentService.CreateBlogComment(request);
                if (response == null)
                {
                    return CustomResult("Error!, have some thing wrong when create blog comment", System.Net.HttpStatusCode.BadRequest);
                }
                return CustomResult("Create Successfully!", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("UpdateBlogComment/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlogComment([FromForm] BlogCommentRequest request, long id)
        {
            try
            {
                var response = await _blogCommentService.UpdateBlogComment(request, id);
                if (response == null)
                {
                    return CustomResult("Error!, have some thing wrong when create blog comment", System.Net.HttpStatusCode.BadRequest);
                }
                return CustomResult("Update Successfully!", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlogComment(long id)
        {
            try
            {
                var response = await _blogCommentService.DeleteBlogComment(id);
                if (!response)
                {
                    return CustomResult("Delete is not success !", System.Net.HttpStatusCode.BadRequest);
                }
                return CustomResult("Delete successfully", response);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }}
