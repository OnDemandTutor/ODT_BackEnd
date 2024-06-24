using System;
using System.Net;
using System.Threading.Tasks;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.Question;


[Route("api/[controller]")]
[ApiController]
public class QuestionRatingController : BaseController
{
    private readonly IQuestionRatingService _questionRatingService;
 
    
    public QuestionRatingController (IQuestionRatingService questionRatingService)
    {
        this._questionRatingService = questionRatingService;
    }

    [HttpGet("GetAllQuestionsRatingByQuestionId/{questionId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllQuestionsRatingByQuestionId(long questionId)
    {
        try
        {
            var questionRatings = await _questionRatingService.GetAllQuestionRatingByQuestionId(questionId);
            return CustomResult("Ok", questionRatings);
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
    
    

    [HttpPost("Like")]
    [Authorize]
        public async Task<IActionResult> Like([FromBody] QuestionRatingRequest questionRatingRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }


                var createdQuestion = await _questionRatingService.LikeQuestion(questionRatingRequest);



                return CustomResult("Created successfully", createdQuestion);
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

        [HttpDelete("Unlike")]
        [Authorize]
        public async Task<IActionResult> Unlike([FromBody] QuestionRatingRequest questionRatingRequest)
        {
            try
            {
                await _questionRatingService.UnlikeQuestion(questionRatingRequest);
                return CustomResult("Delete question successfully", HttpStatusCode.NoContent);
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