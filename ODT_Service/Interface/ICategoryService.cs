using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using Tools;

namespace ODT_Service.Interface;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategories(QueryObject queryObject);

    Task<CategoryResponse> GetCategoryByIdAsync(long id);

    Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest);
        
    Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest categoryRequest, long categoryId);
        
    Task<bool> DeleteCategoryAsync(long questionId);

}