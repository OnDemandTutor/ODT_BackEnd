using System.ComponentModel.DataAnnotations;

namespace ODT_Model.DTO.Request;

public class QuestionRatingRequest
{
    
    [Required (ErrorMessage = "Question Id is required!")]
    public long QuestionId { get; set; }
    
}