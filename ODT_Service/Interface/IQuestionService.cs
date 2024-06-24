using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System.Collections.Generic;

using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionResponse>> GetAllQuestionsAsync(QueryObject queryObject);

        Task<QuestionResponse> GetQuestionByIdAsync(long id);
        
        Task<IEnumerable<QuestionResponse>> GetAllQuestionsByUserId(QueryObject queryObject, long userId);


        Task<QuestionResponse> CreateQuestionWithSubscription(QuestionRequest questionRequest);
        
        Task<QuestionResponse> CreateQuestionByCoin(QuestionRequest questionRequest);

        
        Task<QuestionResponse> UpdateQuestionAsync(QuestionRequest questionRequest, long questionId);
        
        Task<bool> DeleteQuestionAsync(long questionId);

        Task<bool> IsExistByQuestionId(long questionId);

    }
}
