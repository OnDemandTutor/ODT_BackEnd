using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Model.Enum;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Service.Service;

public class QuestionCommentService : IQuestionCommentService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public QuestionCommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;

    }
    
    public async Task<IEnumerable<QuestionCommentResponse>> GetAllQuestionComments(QueryObject queryObject)
    {
        //check if QueryObject search is not null
        Expression<Func<QuestionComment, bool>> filter = null;
        if (!string.IsNullOrWhiteSpace(queryObject.Search))
        {
            filter = questionComment => questionComment.Content.Contains(queryObject.Search);
        }
        
        var questionComments =  _unitOfWork.QuestionCommentRepository.Get(
            filter:filter
            ,includeProperties: "Question", pageIndex:queryObject.PageIndex, pageSize:queryObject.PageSize);
        if (questionComments.IsNullOrEmpty())
        {
            throw new CustomException.DataNotFoundException("The question comment list is empty!");
        }
        var response = _mapper.Map<IEnumerable<QuestionCommentResponse>>(questionComments);
        
        foreach (var comment in response)
        {
            IsMentorFromUserId(comment);
            var question = _unitOfWork.QuestionRepository.Get(q => q.Id == comment.QuestionId, includeProperties:"Category").FirstOrDefault();
            comment.QuestionResponse = _mapper.Map<QuestionResponse>(question);
        }
        return response;

    }

    public async Task<IEnumerable<QuestionCommentResponse>> GetAllQuestionCommentsByQuestionId(QueryObject queryObject, long questionId)
    {
        //check if QueryObject search is not null
        Expression<Func<QuestionComment, bool>> filter = questionComment => questionComment.QuestionId == questionId;
        if (!string.IsNullOrWhiteSpace(queryObject.Search))
        {
            filter = questionComment => questionComment.Content.ToLower().Contains(queryObject.Search.Trim().ToLower()) && questionComment.QuestionId == questionId;
        }
       
        var questionComments =  _unitOfWork.QuestionCommentRepository.Get(
            filter:filter
            ,includeProperties: "Question", pageIndex:queryObject.PageIndex, pageSize:queryObject.PageSize);

        if (questionComments.IsNullOrEmpty())
        {
            throw new CustomException.DataNotFoundException("The question comment list is empty!");
        }
        var response = _mapper.Map<IEnumerable<QuestionCommentResponse>>(questionComments);
        foreach (var comment in response)
        {
            IsMentorFromUserId(comment);
            var question = _unitOfWork.QuestionRepository.Get(q => q.Id == comment.QuestionId, includeProperties:"Category").FirstOrDefault();
            comment.QuestionResponse = _mapper.Map<QuestionResponse>(question);
        }
        return response;
    }

    public async Task<QuestionCommentResponse> GetQuestionCommentById(long id)
    {
        var questionComment = await _unitOfWork.QuestionCommentRepository.GetByIdWithInclude(id, includeProperties:"Question");
        if (questionComment == null)
        {
            throw new CustomException.DataNotFoundException($"Question Comment ID: {id} not found");
        }
        var response = _mapper.Map<QuestionCommentResponse>(questionComment);
        IsMentorFromUserId(response);
        var question = _unitOfWork.QuestionRepository.Get(q => q.Id == response.QuestionId, includeProperties:"Category").FirstOrDefault();
        response.QuestionResponse = _mapper.Map<QuestionResponse>(question);
        return response;
    }

    public async Task<QuestionCommentResponse> CreateQuestionComment(QuestionCommentRequest questionCommentRequest)
    {
        var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));

        if (_unitOfWork.QuestionRepository.GetByID(questionCommentRequest.QuestionId) == null)
        {
            throw new CustomException.DataNotFoundException($"Question with this ID: {questionCommentRequest.QuestionId} not found!");
        }
        if (_unitOfWork.UserRepository.GetByID(userId) == null)
        {
            throw new CustomException.DataNotFoundException($"User with this ID: {userId} not found!");
        }

        var questionComment = _mapper.Map<QuestionComment>(questionCommentRequest);
        questionComment.UserId = userId;
        questionComment.CreateDate = DateTime.Now;
        questionComment.Status = true;
        _unitOfWork.QuestionCommentRepository.Insert(questionComment);
        _unitOfWork.Save();

        var response = _mapper.Map<QuestionCommentResponse>(questionComment);
        IsMentorFromUserId(response);
        var question = _unitOfWork.QuestionRepository.Get(q => q.Id == response.QuestionId, includeProperties:"Category").FirstOrDefault();
        response.QuestionResponse = _mapper.Map<QuestionResponse>(question);
        return response;
    }

    public async Task<QuestionCommentResponse> UpdateQuestionComment(QuestionCommentRequest questionCommentRequest, long questionCommentId)
    {
        var questionComment = _unitOfWork.QuestionCommentRepository.GetByID(questionCommentId);
        var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
        if (questionComment == null)
        {
            throw new CustomException.DataNotFoundException($"Question Comment with ID: {questionCommentId} not found");
        }
        
        if (_unitOfWork.QuestionRepository.GetByID(questionCommentRequest.QuestionId) == null)
        {
            throw new CustomException.DataNotFoundException($"Question with this ID: {questionCommentRequest.QuestionId} not found!");
        }
        if (_unitOfWork.UserRepository.GetByID(userId) == null)
        {
            throw new CustomException.DataNotFoundException($"User with this ID: {userId} not found!");
        }

        if (questionComment.UserId != userId)
        {
            throw new CustomException.InvalidDataException("Invalid input");
        }

        _mapper.Map(questionCommentRequest, questionComment);
        questionComment.ModifiedDate = DateTime.Now;
        _unitOfWork.QuestionCommentRepository.Update(questionComment);
        _unitOfWork.Save();

        var response = _mapper.Map<QuestionCommentResponse>(questionComment);
        IsMentorFromUserId(response);
        var question = _unitOfWork.QuestionRepository.Get(q => q.Id == response.QuestionId, includeProperties:"Category").FirstOrDefault();
        response.QuestionResponse = _mapper.Map<QuestionResponse>(question);
        return response;
    }

    public async Task<bool> DeleteQuestionComment(long questionCommentId)
    {
        var deletedQuestionComment = _unitOfWork.QuestionCommentRepository.GetByID(questionCommentId);

        if (deletedQuestionComment == null)
        {
            throw new CustomException.DataNotFoundException($"Question Comment with ID: {questionCommentId} not found");
        }
            
        _unitOfWork.QuestionCommentRepository.Delete(deletedQuestionComment);
        _unitOfWork.Save();
        return true;
    }
    
    private void IsMentorFromUserId(QuestionCommentResponse response)
    {
        if (response != null) // Check if comment and UserId are not null
        {
            var role = _unitOfWork.RoleRepository.GetByID(_unitOfWork.UserRepository.GetByID(response.UserId).RoleId);
            if (role != null) // Check if role are not null
            {
                response.IsMentor = role.RoleName == RoleName.Mentor.ToString();
            }
        }
    }
}