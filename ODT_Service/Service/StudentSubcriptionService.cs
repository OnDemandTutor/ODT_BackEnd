using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class StudentSubcriptionService : IStudentSubcriptionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentSubcriptionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentSubcriptionResponse>> GetAllStudentSubcription(QueryObject queryObject)
        {
            var getall = _unitOfWork.StudentSubcriptionRepository.Get(
                filter: p => p.Status == true, includeProperties: "Student,Subcription",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!getall.Any())
            {
                throw new CustomException.DataNotFoundException("No StudentSubcription in Database");
            }

            var studentSubcriptionResponses = _mapper.Map<IEnumerable<StudentSubcriptionResponse>>(getall);

            return studentSubcriptionResponses;
        }

        public async Task<StudentSubcriptionResponse> GetStudentSubcriptionByID(long id)
        {

            var studentsubcriptionByID = _unitOfWork.StudentSubcriptionRepository.Get(
            filter: p => p.Id == id && p.Status == true, includeProperties: "Student,Subcription").FirstOrDefault();
            if (studentsubcriptionByID == null)
            {
                throw new CustomException.DataNotFoundException("ID Is not Found.");
            }
            var studentsubcriptionRS = _mapper.Map<StudentSubcriptionResponse>(studentsubcriptionByID);
            return await Task.FromResult(studentsubcriptionRS);
        }

        public async Task<StudentSubcriptionResponse> CreateStudentSubcription(CreateStudentSubcriptionRequest studentSubcriptionRequest)
        {
            var studentsub = _mapper.Map<StudentSubcription>(studentSubcriptionRequest);
            var student = _unitOfWork.StudentRepository.Get(s => s.UserId == studentSubcriptionRequest.UserId).FirstOrDefault();
            if (student == null)
            {
                throw new CustomException.DataNotFoundException("This User is not a student!!");
            }
            // Set trạng thái (limt cứng 20, câu đầu là 0)
            if (studentsub.CurrentQuestion >= 20)
            {
                throw new CustomException.InvalidDataException("You Subcription has been không xài được địt mẹ mày.");
            }
            else
            {
                studentsub.StudentId = student.Id;
                studentsub.CurrentQuestion = 0;
                studentsub.CurrentMeeting = 0;
                studentsub.StartDate = DateTime.Now;
                studentsub.EndDate = DateTime.Now.AddMonths(2);
                // set hiện tại là true có thể sửa thành false nếu muốn
                studentsub.Status = true;

                await _unitOfWork.StudentSubcriptionRepository.AddAsync(studentsub);
            }

            //map request vào response
            StudentSubcriptionResponse studentSubcriptionResponse = _mapper.Map<StudentSubcriptionResponse>(studentsub);
            return studentSubcriptionResponse;
        }

        public async Task<StudentSubcriptionResponse> UpdateStudentSubcription(long id, UpdateStudentSubcriptionRequest updateStudentSubcriptionRequest)
        {
            // Tạo Hàm gọi xử lí studentSubcription
            var existstudentsub = _unitOfWork.StudentSubcriptionRepository.GetByID(id);
            if (existstudentsub == null)
            {
                throw new CustomException.DataNotFoundException("StudentSubcription ID is not exist");
            }

            // Tạo Hàm gọi xử lí Subcription
            var subcription = _unitOfWork.SubcriptionRepository.GetByID(id);
            if (subcription == null)
            {
                throw new CustomException.DataNotFoundException("Subcription ID is not exist");
            }
            _mapper.Map(updateStudentSubcriptionRequest, existstudentsub);

            // Xử lí câu hỏi của student nếu vượt = cúc ngược lại thì cập nhật
            if (existstudentsub.Status == false)
            {
                throw new CustomException.DataNotFoundException("Your Subcription Doesn't exist");
            }
            else if (existstudentsub.CurrentQuestion > subcription.LimitQuestion)
            {
                throw new CustomException.DataNotFoundException("The Current Questions are out");
            }
            else if (existstudentsub.CurrentMeeting > subcription.LimitMeeting)
            {
                throw new CustomException.DataNotFoundException("The Current Meetings are out");
            }
            else
            {
                await _unitOfWork.StudentSubcriptionRepository.UpdateAsync(existstudentsub);
                _unitOfWork.Save();
            }
            var studentSubcriptionResponse = _mapper.Map<StudentSubcriptionResponse>(existstudentsub);
            return studentSubcriptionResponse;
        }

        public async Task<StudentSubcriptionResponse> DeleteStudentSubcription(long id, UpdateStudentSubcriptionRequest updateStudentSubcriptionRequest)
        {
            var existstudentsub = _unitOfWork.StudentSubcriptionRepository.GetByID(id);
            if (existstudentsub == null)
            {
                throw new CustomException.DataNotFoundException("StudentSubcription ID is not exist");
            }
            _mapper.Map(updateStudentSubcriptionRequest, existstudentsub);

            // set trạng thái nếu trả lời câu hỏi => cộng currentQuestion + 1
            if (existstudentsub.Status == false)
            {
                throw new CustomException.DataNotFoundException("Your Subcription Has Been Deleted");
            }
            else
            {
                existstudentsub.Status = false;
                await _unitOfWork.StudentSubcriptionRepository.UpdateAsync(existstudentsub);
                _unitOfWork.Save();
            }
            var studentSubcriptionResponse = _mapper.Map<StudentSubcriptionResponse>(existstudentsub);
            return studentSubcriptionResponse;
        }
    }
}

