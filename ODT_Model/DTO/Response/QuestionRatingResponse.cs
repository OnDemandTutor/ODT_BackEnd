namespace ODT_Model.DTO.Response;

public class QuestionRatingResponse
{
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public long QuestionId { get; set; }

    public bool Status { get; set; }
    
    public QuestionResponse Question { get; set; } 
}
