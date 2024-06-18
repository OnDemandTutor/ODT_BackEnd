using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.Category;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories([FromQuery] QueryObject queryObject)
        {
            try
            {
                var categories = await _categoryService.GetAllCategories(queryObject);
                return CustomResult("Data loaded!", categories);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return CustomResult("Category not found", HttpStatusCode.NotFound);
            }

            return CustomResult("Data loaded!", category);

        }

    [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }


                var createCategory = await _categoryService.CreateCategoryAsync(categoryRequest);



                return CustomResult("Created successfully", createCategory);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
                
            
        }

        [HttpPost("UpdateCategory/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(long categoryId, [FromBody] CategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var updateCategory = await _categoryService.UpdateCategoryAsync(categoryRequest, categoryId);
                return CustomResult("Update successfully", updateCategory);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }

            
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return CustomResult("Delete category successfully");
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }

        }
    
    
}