using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interfaces
{
    public interface IStudentSubcriptionService
    {
        Task<StudentSubcriptionResponse> CreateStudentSubcription(CreateStudentSubcriptionRequest studentSubcriptionRequest);
        Task<StudentSubcriptionResponse> DeleteStudentSubcription(long id, UpdateStudentSubcriptionRequest updateStudentSubcriptionRequest);
        Task<IEnumerable<StudentSubcriptionResponse>> GetAllStudentSubcription(QueryObject queryObject);
        Task<StudentSubcriptionResponse> GetStudentSubcriptionByID(long id);
        Task<IEnumerable<StudentSubcriptionResponse>> GetStudentSubcriptionByUserID(long userId);
        Task<StudentSubcriptionResponse> UpdateStudentSubcription(long id, UpdateStudentSubcriptionRequest updateStudentSubcriptionRequest);
    }
}
