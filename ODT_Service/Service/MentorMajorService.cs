using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using Tools;

namespace ODT_Service.Service
{
    public class MentorMajorService : IMentorMajorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MentorMajorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MentorMajorResponse>> GetAllMentorMajor(QueryObject queryObject)
        {
            var mms = _unitOfWork.MentorMajorRepository.Get(includeProperties: "Mentor,Major",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!mms.Any())
            {
                throw new CustomException.DataNotFoundException("No MentorMajor in Database");
            }

            var mentorMajorResponses = _mapper.Map<IEnumerable<MentorMajorResponse>>(mms);

            return mentorMajorResponses;
        }

        public async Task<List<MentorMajorResponse>> GetAllMajorByMentorId(long id)
        {
            var mms = _unitOfWork.MentorMajorRepository.Get(filter: p =>
                                                        p.MentorId == id, includeProperties: "Mentor,Major");

            if (mms == null)
            {
                throw new CustomException.DataNotFoundException($"MentorMajor not found with MentorId: {id}");
            }

            var mentorMajorResponses = _mapper.Map<List<MentorMajorResponse>>(mms);
            return mentorMajorResponses;
        }

        public async Task<List<MentorMajorResponse>> GetAllMentorMajorByMentorId(long id)
        {
            var mms = _unitOfWork.MentorMajorRepository.Get(filter: p =>
                                                        p.MentorId == id, includeProperties: "Mentor,Major");

            if (mms == null)
            {
                throw new CustomException.DataNotFoundException($"MentorMajor not found with MentorId: {id}");
            }

            var mentorMajorResponses = _mapper.Map<List<MentorMajorResponse>>(mms);
            return mentorMajorResponses;
        }

        public async Task<List<MentorMajorResponse>> GetAllMentorByMajorId(long id)
        {
            var mms = _unitOfWork.MentorMajorRepository.Get(filter: p =>
                                                        p.MajorId == id, includeProperties: "Mentor,Major");

            if (mms == null)
            {
                throw new CustomException.DataNotFoundException($"MentorMajor not found with MajorId: {id}");
            }

            var mentorMajorResponses = _mapper.Map<List<MentorMajorResponse>>(mms);
            return mentorMajorResponses;
        }

        public async Task<List<MentorMajorResponse>> GetMentorMajorById(long id)
        {
            var mm = _unitOfWork.MentorMajorRepository.Get(filter: p =>
                                            p.Id == id, includeProperties: "Mentor,Major");

            if (mm == null)
            {
                throw new CustomException.DataNotFoundException($"MentorMajor not found with ID: {id}");
            }

            var mentorMajorResponse = _mapper.Map<List<MentorMajorResponse>>(mm);
            return mentorMajorResponse;
        }

        public async Task<MentorMajorResponse> CreateMentorMajor(MentorMajorRequest mentorMajorRequest)
        {
            var mentor = _unitOfWork.MentorRepository.GetByID(mentorMajorRequest.MentorId);

            if (mentor == null)
            {
                throw new CustomException.DataNotFoundException($"Mentor not found with ID: {mentorMajorRequest.MentorId}");
            }

            var major = _unitOfWork.MajorRepository.GetByID(mentorMajorRequest.MajorId);

            if (major == null)
            {
                throw new CustomException.DataNotFoundException($"Major not found with ID: {mentorMajorRequest.MajorId}");
            }

            var existingMM = _unitOfWork.MentorMajorRepository.Get().FirstOrDefault(p =>
                                                                p.MentorId == mentorMajorRequest.MentorId &&
                                                                p.MajorId == mentorMajorRequest.MajorId);

            if (existingMM != null)
            {
                throw new CustomException.DataExistException($"MentorMajor with Id '{mentorMajorRequest.MajorId}' already exists.");
            }
            var mentorMajorResponse = _mapper.Map<MentorMajorResponse>(existingMM);
            var newMM = _mapper.Map<MentorMajor>(mentorMajorRequest);

            _unitOfWork.MentorMajorRepository.Insert(newMM);
            _unitOfWork.Save();

            _mapper.Map(newMM, mentorMajorResponse);
            return mentorMajorResponse;
        }

        public async Task<bool> DeleteMentorMajor(long id)
        {
            try
            {
                var mentorMajor = _unitOfWork.MentorMajorRepository.GetByID(id);
                if (mentorMajor == null)
                {
                    throw new CustomException.DataNotFoundException("MentorMajor not found.");
                }

                _unitOfWork.MentorMajorRepository.Delete(mentorMajor);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
