using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Tools;
using ODT_Service.Interface;
using ODT_Model.DTO.Request;


namespace ODT_API.Controllers.Question

{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }


        [HttpGet("GetAllQuestions")]
        public async Task<IActionResult> GetAllQuestions([FromQuery]QueryObject queryObject)
        {
            try
            {
                var questions = await _questionService.GetAllQuestionsAsync(queryObject);
                return CustomResult("Data loaded!", questions);
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


        [HttpGet("GetQuestionById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetQuestionById(long id)
        {
            try
            {
                var question = await _questionService.GetQuestionByIdAsync(id);
                return CustomResult("Data loaded!", question);
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

        [HttpGet("GetAllQuestionsByUserId/{id}")]
        public async Task<IActionResult> GetAllQuestionsByUserId([FromQuery]QueryObject queryObject, long id)
        {
            try
            {
                var questions = await _questionService.GetAllQuestionsByUserId(queryObject, id);
                return CustomResult("Data loaded!", questions);
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

    [HttpPost("CreateQuestionWithSubscription")]
    [Authorize]
        public async Task<IActionResult> CreateQuestionWithSubscription([FromForm] QuestionRequest questionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }


                var createdQuestion = await _questionService.CreateQuestionWithSubscription(questionRequest);



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

        [HttpPost("CreateQuestionByCoin")]
        [Authorize]
        public async Task<IActionResult> CreateQuestionByCoin([FromForm] QuestionRequest questionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }


                var createdQuestion = await _questionService.CreateQuestionByCoin(questionRequest);



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
        
        [HttpPatch("UpdateQuestion/{questionId}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuestion(long questionId, [FromForm] QuestionRequest questionRequest)
        {
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var updateQuestion = await _questionService.UpdateQuestionAsync(questionRequest, questionId);
                return CustomResult("Update successfully", updateQuestion);
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

        [HttpDelete("DeleteQuestion/{questionId}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(long questionId)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(questionId);
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



}
