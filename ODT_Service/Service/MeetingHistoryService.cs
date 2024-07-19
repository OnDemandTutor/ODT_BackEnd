using AutoMapper;
using ODT_Model.DTO.Response;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class MeetingHistoryService : IMeetingHistory
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MeetingHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MeetingHistoryResponse>> GetAllMeetingHistory(QueryObject queryObject)
        {
            var meetingHistories = _unitOfWork.MeetingHistoryRepository.Get(includeProperties: "Student,Mentor",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!meetingHistories.Any())
            {
                throw new CustomException.DataNotFoundException("No MeetingHistory in Database");
            }

            var meetingHistoryResponses = _mapper.Map<IEnumerable<MeetingHistoryResponse>>(meetingHistories);

            return meetingHistoryResponses;
        }

        public async Task<List<MeetingHistoryResponse>> GetAllMeetingHistoryByStudentId(long id)
        {
            var meetingHistories = _unitOfWork.MeetingHistoryRepository.Get(filter: p =>
                                                        p.StudentId == id, includeProperties: "Student,Mentor");

            if (meetingHistories == null)
            {
                throw new CustomException.DataNotFoundException($"MeetingHistory not found with StudentId: {id}");
            }

            var meetingHistoryResponses = _mapper.Map<List<MeetingHistoryResponse>>(meetingHistories);
            return meetingHistoryResponses;
        }

        public async Task<List<MeetingHistoryResponse>> GetMeetingHistoryById(long id)
        {
            var meetingHistories = _unitOfWork.MeetingHistoryRepository.Get(filter: p =>
                                                        p.MentorId == id, includeProperties: "Student,Mentor");

            if (meetingHistories == null)
            {
                throw new CustomException.DataNotFoundException($"MeetingHistory not found with MentorId: {id}");
            }

            var meetingHistoryResponses = _mapper.Map<List<MeetingHistoryResponse>>(meetingHistories);
            return meetingHistoryResponses;
        }

        public async Task<List<MeetingHistoryResponse>> GetMeetingHistoryByMentorId(long id)
        {
            var meetingHistories = _unitOfWork.MeetingHistoryRepository.Get(filter: p =>
                                                        p.Id == id, includeProperties: "Student,Mentor");

            if (meetingHistories == null)
            {
                throw new CustomException.DataNotFoundException($"MeetingHistory not found with Id: {id}");
            }

            var meetingHistoryResponses = _mapper.Map<List<MeetingHistoryResponse>>(meetingHistories);
            return meetingHistoryResponses;
        }
    }
}
