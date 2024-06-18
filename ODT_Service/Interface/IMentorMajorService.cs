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
    public interface IMentorMajorService
    {
        Task<IEnumerable<MentorMajorResponse>> GetAllMentorMajor(QueryObject queryObject);
        Task<List<MentorMajorResponse>> GetAllMajorByMentorId(long id);
        Task<List<MentorMajorResponse>> GetAllMentorByMajorId(long id);
        Task<List<MentorMajorResponse>> GetMentorMajorById(long id);
        Task<MentorMajorResponse> CreateMentorMajor(MentorMajorRequest mentorMajorRequest);
        Task<bool> DeleteMentorMajor(long id);
    }
}
