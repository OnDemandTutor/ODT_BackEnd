using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Service.Service;

public class QuestionRatingService : IQuestionRatingService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public QuestionRatingService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;

    }
    public async Task<IEnumerable<QuestionRatingResponse>> GetAllQuestionsRatings()
    {
        var questionRatings = _unitOfWork.QuestionRatingRepository.Get();
        return _mapper.Map<IEnumerable<QuestionRatingResponse>>(questionRatings);
    }

    public async Task<QuestionRatingResponse> GetQuestionRatingById(long id)
    {
        var question = _unitOfWork.QuestionRatingRepository.GetByID(id);
        return _mapper.Map<QuestionRatingResponse>(question);
    }

    public async Task<QuestionRatingResponse> LikeQuestion(QuestionRatingRequest questionRatingRequest)
    {
        var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));

        if (_unitOfWork.UserRepository.GetByID(userId) == null)
        {
            throw new CustomException.DataNotFoundException($"Student with this {userId} not found!");
        }
        if (_unitOfWork.QuestionRepository.GetByID(questionRatingRequest.QuestionId) == null)
        {
            throw new CustomException.DataNotFoundException($"Category with this {questionRatingRequest.QuestionId} not found!");
        }

        //create question rating
        var questionRating = _mapper.Map<QuestionRating>(questionRatingRequest);
        //check if rating is exist
        bool isExist = await RatingExists(questionRating.QuestionId, userId);
        if (isExist)
        {
            throw new CustomException.InvalidDataException("500", "This Rating is duplicated!");
        }
        questionRating.Status = true;
        questionRating.UserId = userId;
        _unitOfWork.QuestionRatingRepository.Insert(questionRating);
        var question = _unitOfWork.QuestionRepository.Get(q => q.Id == questionRatingRequest.QuestionId, includeProperties:"Category").FirstOrDefault();
        //add total like into question
        question.TotalRating++;
        _unitOfWork.QuestionRepository.Update(question);
        _unitOfWork.Save();

        var response = _mapper.Map<QuestionRatingResponse>(questionRating);
        response.Question = _mapper.Map<QuestionResponse>(question);
        
        return response;
    }

    public async Task UnlikeQuestion(QuestionRatingRequest questionRatingRequest)
    {
        var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));

        if (_unitOfWork.UserRepository.GetByID(userId) == null)
        {
            throw new CustomException.DataNotFoundException($"Student with this {userId} not found!");
        }
        if (_unitOfWork.QuestionRepository.GetByID(questionRatingRequest.QuestionId) == null)
        {
            throw new CustomException.DataNotFoundException($"Category with this {questionRatingRequest.QuestionId} not found!");
        }
        
        //check if rating is exist
        bool isExist = await RatingExists(questionRatingRequest.QuestionId, userId);
        if (!isExist)
        {
            throw new CustomException.DataNotFoundException($"This QuestionRating not found!");

        }
        var questionRating = _unitOfWork.QuestionRatingRepository.Get(rating => rating.QuestionId == questionRatingRequest.QuestionId 
            && rating.UserId == userId).FirstOrDefault();
        //delete question rating
        _unitOfWork.QuestionRatingRepository.Delete(questionRating);
        
        //subtract question total rating
        var question = _unitOfWork.QuestionRepository.GetByID(questionRatingRequest.QuestionId);
        question.TotalRating--;
        _unitOfWork.QuestionRepository.Update(question);
        _unitOfWork.Save();

    }

    public async Task<IEnumerable<QuestionRatingResponse>> GetAllQuestionRatingByQuestionId(long questionId)
    {
        if (!await _unitOfWork.QuestionRepository.ExistsAsync(q => q.Id == questionId))
        {
            throw new CustomException.DataNotFoundException($"Question with Id: {questionId} not found!");
        }
        var questionRatings =_unitOfWork.QuestionRatingRepository.Get(rating => rating.QuestionId == questionId, includeProperties:"Question");
        if (questionRatings == null)
        {
            throw new CustomException.DataNotFoundException($"The question rating list is empty!");

        }

        var ratingResponses = _mapper.Map<IEnumerable<QuestionRatingResponse>>(questionRatings);
        foreach (var response in ratingResponses)
        {
            var question = _unitOfWork.QuestionRepository.Get(q => q.Id == response.QuestionId, includeProperties:"Category").FirstOrDefault();
            response.Question = _mapper.Map<QuestionResponse>(question);
        }
        return ratingResponses;
    }

    public async Task<bool> RatingExists(long questionId, long userId)
    {
        return await _unitOfWork.QuestionRatingRepository.ExistsAsync(r => 
            r.QuestionId == questionId && r.UserId == userId);
    }
}