using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace ODT_Service.Interface;

public interface IQuestionRatingService
{
    Task<IEnumerable<QuestionRatingResponse>> GetAllQuestionsRatings();
    Task<QuestionRatingResponse> GetQuestionRatingById(long id);
    Task<QuestionRatingResponse> LikeQuestion(QuestionRatingRequest questionRatingRequest);
    Task UnlikeQuestion(QuestionRatingRequest questionRatingRequest);
    Task<IEnumerable<QuestionRatingResponse>> GetAllQuestionRatingByQuestionId(long questionId);
}