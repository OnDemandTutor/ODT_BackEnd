using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IMajorService
    {
        Task<IEnumerable<MajorResponse>> GetAllMajor(QueryObject queryObject);
        Task<MajorResponse> GetMajorById(long id);
        Task<MajorResponse> CreateMajor(MajorRequest majorRequest);
        Task<MajorResponse> UpdateMajor(long id, MajorRequest majorRequest);
        Task<bool> DeleteMajor(long id);
    }
}
