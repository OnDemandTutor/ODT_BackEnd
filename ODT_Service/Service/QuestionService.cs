using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Tools;
using ODT_Service.Interface;
using ODT_Repository.Repository;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Model.DTO.Request;

namespace ODT_Service.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Tools.Firebase _firebase;
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, Tools.Firebase firebase)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _firebase = firebase;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionsAsync(QueryObject queryObject)
        {
            //check if QueryObject search is not null
            Expression<Func<Question, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = question => question.Content.Contains(queryObject.Search);
            }

            var questions = _unitOfWork.QuestionRepository.Get(
                filter: filter,
                includeProperties:"Category",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize);
            if (questions.IsNullOrEmpty())
            {
                throw new CustomException.DataNotFoundException("The question list is empty!");
            }

            var questionResponses = _mapper.Map<IEnumerable<QuestionResponse>>(questions);
            foreach (var question in questionResponses)
            {
                question.UserId = GetUserIdByStudentId(question.StudentId);
            }
        
            return questionResponses;
        }

        public async Task<QuestionResponse> GetQuestionByIdAsync(long id)
        {
            var question = _unitOfWork.QuestionRepository.GetByID(id);


            if (question == null)
            {
                throw new CustomException.DataNotFoundException($"Question with ID: {id} not found!");

            }
            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == question.CategoryId).FirstOrDefault();
            if (category != null) question.Category = category;
            var questionResponse = _mapper.Map<QuestionResponse>(question);
            questionResponse.UserId = GetUserIdByStudentId(question.StudentId);
            return questionResponse; 

        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionsByUserId(QueryObject queryObject, long userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user == null)
            {
                throw new CustomException.DataNotFoundException("This User is not exist!");
            }

            var student = _unitOfWork.StudentRepository.Get(s => s.UserId == userId).FirstOrDefault();
            if (student == null)
            {
                throw new CustomException.DataNotFoundException("This User is not a student!");

            }
            
            //check if QueryObject search is not null
            Expression<Func<Question, bool>> filter = question => question.StudentId == student.Id;
            ;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = question => question.StudentId == student.Id && question.Content.Contains(queryObject.Search);
            }
            var questions = _unitOfWork.QuestionRepository.Get(
                filter: filter,
                includeProperties:"Category",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize);
            if (questions.IsNullOrEmpty())
            {
                throw new CustomException.DataNotFoundException("The question list is empty!");
            }
            
            var questionResponses = _mapper.Map<IEnumerable<QuestionResponse>>(questions);
            foreach (var question in questionResponses)
            {
                question.UserId = GetUserIdByStudentId(question.StudentId);
            }
        
            return questionResponses;
        }


        public async Task<QuestionResponse> CreateQuestionWithSubscription(QuestionRequest questionRequest)
        {
            var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
            var studentId = GetStudentIdByUserId(userId);
            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryName == questionRequest.CategoryName).FirstOrDefault();
            if (_unitOfWork.StudentRepository.GetByID(studentId) == null)
            {
                throw new CustomException.DataNotFoundException($"Student with ID: {studentId} not found!");
            }
            if (category == null)
            {
                throw new CustomException.DataNotFoundException($"Category: {questionRequest.CategoryName} not found!");
            }
            //check if student having subscription
            
            var studentSubscription = _unitOfWork.StudentSubcriptionRepository
                .Get(s => s.StudentId == studentId && s.Status == true).FirstOrDefault();

            if (studentSubscription == null)
            {
                throw new CustomException.InvalidDataException("This Student does not have subscription");
            }

            var subscription = _unitOfWork.SubcriptionRepository.GetByID(studentSubscription.SubcriptionId);

            //check if current question equal to limit subscription
            if (studentSubscription.CurrentQuestion == subscription.LimitQuestion)
            {
                throw new CustomException.InvalidDataException("The student's current question have reach limit!");
            }
            
            studentSubscription.CurrentQuestion++;
            _unitOfWork.StudentSubcriptionRepository.Update(studentSubscription);
            //save to database
            var questionWithSubscription = _mapper.Map<Question>(questionRequest);
            if (questionRequest.Image != null)
            {
                string[] imgExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!imgExtensions.Contains(Path.GetExtension(questionRequest.Image.FileName)))
                {
                    throw new CustomException.InvalidDataException("Just accept image!");
                }
                var imageurl = await _firebase.UploadImageAsync(questionRequest.Image);
                questionWithSubscription.Image = imageurl;
            }
            else
            {
                questionWithSubscription.Image = null;

            }

            questionWithSubscription.CreateDate = DateTime.Now;
            questionWithSubscription.TotalRating = 0;
            questionWithSubscription.StudentId = studentId;
            questionWithSubscription.CategoryId = category.Id;
            _unitOfWork.QuestionRepository.Insert(questionWithSubscription);
            _unitOfWork.Save();
            var questionResponse = _mapper.Map<QuestionResponse>(questionWithSubscription);
            questionResponse.UserId = userId;
            return questionResponse;

        }
        

        public async Task<QuestionResponse> CreateQuestionByCoin(QuestionRequest questionRequest)
        {
            var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
            var studentId = GetStudentIdByUserId(userId);
            var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryName == questionRequest.CategoryName).FirstOrDefault();

            if (_unitOfWork.StudentRepository.GetByID(studentId) == null)
            {
                throw new CustomException.DataNotFoundException($"Student with ID: {studentId} not found!");
            }
            if (category == null)
            {
                throw new CustomException.DataNotFoundException($"Category: {questionRequest.CategoryName} not found!");
            }
            
            
            var student = _unitOfWork.StudentRepository.GetByID(studentId);
            //If having coin
            //20 FuCoin per question
            var userWallet = _unitOfWork.WalletRepository.Get(wallet => wallet.UserId == student.UserId).FirstOrDefault();
            if (userWallet.Balance < 20)
            {
                throw new CustomException.InvalidDataException("You dont have enough FuCoin!");
            }

            userWallet.Balance -= 20;
            //save to database
            var questionWithCoin = _mapper.Map<Question>(questionRequest);
            if (questionRequest.Image != null)
            {
                string[] imgExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!imgExtensions.Contains(Path.GetExtension(questionRequest.Image.FileName)))
                {
                    throw new CustomException.InvalidDataException("Just accept image!");
                }
                var imageurl = await _firebase.UploadImageAsync(questionRequest.Image);
                questionWithCoin.Image = imageurl;
            }
            else
            {
                questionWithCoin.Image = null;

            }
            
            questionWithCoin.CategoryId = category.Id;
            questionWithCoin.StudentId = studentId;
            questionWithCoin.CreateDate = DateTime.Now;
            questionWithCoin.TotalRating = 0;
            _unitOfWork.QuestionRepository.Insert(questionWithCoin);
            _unitOfWork.Save();
            
            var questionResponse = _mapper.Map<QuestionResponse>(questionWithCoin);
            questionResponse.UserId = userId;
            return questionResponse;

        }


        public async Task<QuestionResponse> UpdateQuestionAsync(QuestionRequest questionRequest, long questionId)
        {
            var question = _unitOfWork.QuestionRepository.Get(q => q.Id == questionId, includeProperties: "Category").First();

            if (question == null)
            {
                throw new CustomException.DataNotFoundException($"Question with ID: {questionId} not found");
            }

            if (questionRequest.CategoryName != question.Category.CategoryName)
            {
                var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryName == questionRequest.CategoryName).First();
                question.CategoryId = category.Id;
                _unitOfWork.QuestionRepository.SaveChangesAsync();
            }

            _mapper.Map(questionRequest, question);
            if (questionRequest.Image != null)
            {
                string[] imgExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!imgExtensions.Contains(Path.GetExtension(questionRequest.Image.FileName)))
                {
                    throw new CustomException.InvalidDataException("Just accept image!");
                }
                var imageurl = await _firebase.UploadImageAsync(questionRequest.Image);
                question.Image = imageurl;
            }
            else
            {
                question.Image = null;
            }
            

            question.ModifiedDate = DateTime.Now;
            _unitOfWork.QuestionRepository.Update(question);
            _unitOfWork.Save();

            return _mapper.Map<QuestionResponse>(question);
        }

        public async Task<bool> DeleteQuestionAsync(long questionId)
        {
            var deletedQuestion = _unitOfWork.QuestionRepository.GetByID(questionId);

            if (deletedQuestion == null)
            {
                throw new CustomException.DataNotFoundException($"Question with ID: {questionId} not found");
            }

            _unitOfWork.QuestionRepository.Delete(deletedQuestion);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> IsExistByQuestionId(long questionId)
        {
            var isExist = await _unitOfWork.QuestionRepository.ExistsAsync(question => question.Id == questionId);
            return isExist;
        }

        public long GetStudentIdByUserId(long userId)
        {
            var student = _unitOfWork.StudentRepository.Get(s => s.UserId == userId).FirstOrDefault();
            if (student == null)
            {
                throw new CustomException.DataNotFoundException("This user is not a student <3");
            }
            return student.Id;
        }
        
        public long GetUserIdByStudentId(long studentId)
        {
            var user =  _unitOfWork.StudentRepository.Get(s => s.Id == studentId).FirstOrDefault();
            if (user == null)
            {
                throw new CustomException.DataNotFoundException("This user is not a student <3");
            }
            return user.UserId;
        }
    }
}