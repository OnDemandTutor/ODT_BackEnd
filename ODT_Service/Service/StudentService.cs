using AutoMapper;
using ODT_Model.DTO.Response;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Service
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentResponse>> GetAllStudent(QueryObject queryObject)
        {

            var students = _unitOfWork.StudentRepository.Get(includeProperties: "User",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!students.Any())
            {
                throw new CustomException.DataNotFoundException("No Student in Database");
            }

            var studentResponses = _mapper.Map<IEnumerable<StudentResponse>>(students);

            return studentResponses;
        }

        public async Task<StudentResponse> GetStudentById(long id)
        {
            var student = _unitOfWork.StudentRepository.GetByID(id);

            if (student == null)
            {
                throw new CustomException.DataNotFoundException($"Student not found with ID: {id}");
            }

            var studentResponse = _mapper.Map<StudentResponse>(student);
            return studentResponse;
        }
    }
}
