using System.Linq.Expressions;
using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Repository.Service;

public class CategoryService : ICategoryService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    
    public async Task<IEnumerable<CategoryResponse>> GetAllCategories(QueryObject queryObject)
    {
        //check if QueryObject search is not null
        Expression<Func<Category, bool>> filter = null;
        if (!string.IsNullOrWhiteSpace(queryObject.Search))
        {
            filter = categoty => categoty.CategoryName.Contains(queryObject.Search);
        }
        
        var categories = _unitOfWork.CategoryRepository.Get(
            filter: filter,
            pageIndex:queryObject.PageIndex,
            pageSize:queryObject.PageSize);
        if (categories == null)
        {
            throw new CustomException.DataNotFoundException("The category list is empty!");
        }
        return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
    }

    public async Task<CategoryResponse> GetCategoryByIdAsync(long id)
    {
        var category = _unitOfWork.CategoryRepository.GetByID(id);
        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest categoryRequest)
    {
        bool isExist = await _unitOfWork.CategoryRepository.ExistsAsync(
            category => category.CategoryName.ToLower() == categoryRequest.CategoryName.ToLower());
        if (isExist)
        {
            throw new CustomException.InvalidDataException("500", "This category is duplicated!");
        }
        
        
        var cateogry = _mapper.Map<Category>(categoryRequest);
        _unitOfWork.CategoryRepository.Insert(cateogry);
        _unitOfWork.Save();
        return _mapper.Map<CategoryResponse>(cateogry);
    }

    public async Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest categoryRequest, long categoryId)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);

        if (category == null)
        {
            throw new CustomException.DataNotFoundException($"Category with ID: {categoryId} not found!");
        }
        
        bool isExist = await _unitOfWork.CategoryRepository.ExistsAsync(
            category => category.CategoryName.ToLower() == categoryRequest.CategoryName.ToLower());
        if (isExist)
        {
            throw new CustomException.InvalidDataException("500", "This category is duplicated!");
        }
        
        
        _mapper.Map(categoryRequest, category);
        _unitOfWork.CategoryRepository.Update(category);
        _unitOfWork.Save();

        return _mapper.Map<CategoryResponse>(category);
        
    }

    public async Task<bool> DeleteCategoryAsync(long categoryId)
    {
        var deletedCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        if (deletedCategory == null)
        {
            throw new CustomException.DataNotFoundException($"Category with ID: {categoryId} not found");
        }
            
        _unitOfWork.CategoryRepository.Delete(deletedCategory);
        _unitOfWork.Save();
        return true;
    }
}