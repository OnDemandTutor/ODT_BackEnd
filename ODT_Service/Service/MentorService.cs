using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Model.Enum;
using ODT_Service.Interface;
using Microsoft.AspNetCore.Http;
using Tools;
using ODT_Repository.Repository;

namespace ODT_Service.Service
{
    public class MentorService : IMentorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MentorService(IUnitOfWork unitOfWork, IMapper mapper, Tools.Firebase firebase, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<MentorResponse>> GetAllMentor(QueryObject queryObject)
        {
            var mentors = _unitOfWork.MentorRepository.Get(includeProperties: "User",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!mentors.Any())
            {
                throw new CustomException.DataNotFoundException("No Mentor in Database");
            }

            var mentorResponses = _mapper.Map<IEnumerable<MentorResponse>>(mentors);

            return mentorResponses;
        }

        public async Task<IEnumerable<MentorResponse>> GetAllMentorVerify(QueryObject queryObject)
        {
            var mentors = _unitOfWork.MentorRepository.Get(p => p.VerifyStatus == true, includeProperties: "User",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!mentors.Any())
            {
                throw new CustomException.DataNotFoundException("No Mentor in Database");
            }

            var mentorResponses = _mapper.Map<IEnumerable<MentorResponse>>(mentors);

            return mentorResponses;
        }
        public async Task<IEnumerable<MentorResponse>> GetAllMentorWaiting(QueryObject queryObject)
        {
            var mentors = _unitOfWork.MentorRepository.Get(p => p.VerifyStatus == false, includeProperties: "User",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!mentors.Any())
            {
                throw new CustomException.DataNotFoundException("No Mentor in Database");
            }

            var mentorResponses = _mapper.Map<IEnumerable<MentorResponse>>(mentors);

            return mentorResponses;
        }

        public async Task<List<MentorResponse>> GetMentorById(long id)
        {
            var mentor = _unitOfWork.MentorRepository.Get(p => p.Id == id, includeProperties: "User");

            if (mentor == null)
            {
                throw new CustomException.DataNotFoundException($"Mentor not found with ID: {id}");
            }

            var mentorResponse = _mapper.Map<List<MentorResponse>>(mentor);
            return mentorResponse;
        }

        public async Task<MentorResponse> UpdateMentor(long id, MentorRequest mentorRequest)
        {
            var existingMentor = _unitOfWork.MentorRepository.GetByID(id);

            if (existingMentor == null)
            {
                throw new CustomException.DataNotFoundException($"Mentor with ID {id} not found.");
            }

            if (!existingMentor.VerifyStatus)
            {
                throw new CustomException.InvalidDataException($"Mentor with ID {id} was InActive.");
            }

            if (mentorRequest.File != null)
            {
                if (mentorRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                string imageDownloadUrl = await _firebase.UploadImageAsync(mentorRequest.File);

                if (!string.IsNullOrEmpty(imageDownloadUrl))
                {
                    existingMentor.Video = imageDownloadUrl;
                }
            }
            _mapper.Map(mentorRequest, existingMentor);
            _unitOfWork.MentorRepository.Update(existingMentor);
            _unitOfWork.Save();

            var mentorResponse = _mapper.Map<MentorResponse>(existingMentor);
            return mentorResponse;
        }

        public Task<bool> DeleteMentor(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateMentorOnlineStatusResponse> UpdateOnlineStatus(long id, UpdateMentorOnlineStatusResquest updateMentorOnlineStatusResquest)
        {
            var existingMentor = _unitOfWork.MentorRepository.GetByID(id);

            if (existingMentor == null)
            {
                throw new CustomException.DataNotFoundException($"Mentor with ID {id} not found.");
            }

            if (!existingMentor.VerifyStatus)
            {
                throw new CustomException.InvalidDataException($"Mentor with ID {id} was Inactive.");
            }

            if (!Enum.IsDefined(typeof(OnlineStatus), updateMentorOnlineStatusResquest.OnlineStatus))
            {
                throw new CustomException.InvalidDataException("Invalid Online status value.");
            }

            existingMentor.OnlineStatus = updateMentorOnlineStatusResquest.OnlineStatus;

            _unitOfWork.MentorRepository.Update(existingMentor);
            _unitOfWork.Save();

            var updateMentorOnlineStatusResponse = _mapper.Map<UpdateMentorOnlineStatusResponse>(existingMentor);
            return updateMentorOnlineStatusResponse;
        }
        
        public async Task<MentorResponse> UpdateMentorLoggingIn(MentorRequest mentorRequest)
        { 
            var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
            var mentor = _unitOfWork.MentorRepository.GetByID(userId);

            if (mentor == null)
            {
                throw new CustomException.InvalidDataException("This user is not a mentor!!");
            }
            _mapper.Map(mentorRequest, mentor);
            _unitOfWork.MentorRepository.Update(mentor);
            return _mapper.Map<MentorResponse>(mentor);
        }

        public async Task<MentorResponse> VerifyMentor(long id)
        {
            var existingMentor = _unitOfWork.MentorRepository.GetByID(id);

            if (existingMentor == null)
            {
                throw new CustomException.DataNotFoundException($"Mentor with ID {id} not found.");
            }

            if (existingMentor.VerifyStatus)
            {
                throw new CustomException.InvalidDataException($"Mentor with ID {id} was Active.");
            }

            existingMentor.VerifyStatus = true;

            _unitOfWork.MentorRepository.Update(existingMentor);
            _unitOfWork.Save();

            var mentorResponse = _mapper.Map<MentorResponse>(existingMentor);
            return mentorResponse;
        }
    }
}
