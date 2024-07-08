using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponse>> GetAllStudent(QueryObject queryObject);
        Task<StudentResponse> GetStudentById(long id);
    }
}
