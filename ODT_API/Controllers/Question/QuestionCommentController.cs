using System;
using System.Net;
using System.Threading.Tasks;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using ODT_Model.DTO.Request;
using ODT_Service.Interface;
using Tools;

namespace ODT_API.Controllers.Question;

[Route("api/[controller]")]
[ApiController]
public class QuestionCommentController : BaseController
{
    private readonly IQuestionCommentService _questionCommentService;

    public QuestionCommentController(IQuestionCommentService questionCommentServiceService)
    {
        this._questionCommentService = questionCommentServiceService;
    }


    [HttpGet("GetAllQuestionComments")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllQuestionComments([FromQuery] QueryObject queryObject)
    {
        try
        {
            var questionsComments = await _questionCommentService.GetAllQuestionComments(queryObject);
            return CustomResult("Data Loaded!", questionsComments);
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

    [HttpGet("GetQuestionCommentById/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetQuestionCommentById(long id)
    {
        try
        {
            var question = await _questionCommentService.GetQuestionCommentById(id);
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

    [HttpGet("GetAllQuestionCommentsByQuestionId/{questionId}")]
    public async Task<IActionResult> GetAllQuestionCommentsByQuestionId([FromQuery] QueryObject queryObject, long questionId)
    {
        try
        {
            var questionRatings = await _questionCommentService.GetAllQuestionCommentsByQuestionId(queryObject, questionId);
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

    [HttpPost("CreateQuestionComment")]
    [Authorize]
    public async Task<IActionResult> CreateQuestionComment([FromBody] QuestionCommentRequest questionCommentRequest)
    {
        //try
        //{
            if (!ModelState.IsValid)
            {
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }


            var createdQuestionComment = await _questionCommentService.CreateQuestionComment(questionCommentRequest);


            return CustomResult("Created successfully", createdQuestionComment);
        //}
        /*catch (CustomException.DataNotFoundException e)
        {
            return CustomResult(e.Message, HttpStatusCode.NotFound);
        }
        catch (Exception exception)
        {
            return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
        }*/
    }

    [HttpPatch("UpdateQuestionComment/{questionCommentId}")]
    [Authorize]
    public async Task<IActionResult> UpdateQuestionComment(long questionCommentId,
        [FromBody] QuestionCommentRequest questionRequest)
    {
        if (!ModelState.IsValid)
        {
            return CustomResult(ModelState, HttpStatusCode.BadRequest);
        }

        try
        {
            var updateQuestionComment =
                await _questionCommentService.UpdateQuestionComment(questionRequest, questionCommentId);
            return CustomResult("Update successfully", updateQuestionComment);
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

    [HttpDelete("DeleteQuestionComment/{questionId}")]
    [Authorize]
    public async Task<IActionResult> DeleteQuestionComment(long questionId)
    {
        try
        {
            await _questionCommentService.DeleteQuestionComment(questionId);
            return CustomResult("Delete question successfully", HttpStatusCode.OK);
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