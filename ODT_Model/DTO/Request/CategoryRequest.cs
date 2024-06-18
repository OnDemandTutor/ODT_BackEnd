using System.ComponentModel.DataAnnotations;

namespace ODT_Model.DTO.Request;

public class CategoryRequest
{
    [Required(ErrorMessage = "Category name can not empty")]
    public string CategoryName { get; set; }
}