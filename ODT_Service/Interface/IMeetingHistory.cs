using ODT_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface IMeetingHistory
    {
        Task<IEnumerable<MeetingHistoryResponse>> GetAllMeetingHistory(QueryObject queryObject);
        Task<List<MeetingHistoryResponse>> GetAllMeetingHistoryByStudentId(long id);
        Task<List<MeetingHistoryResponse>> GetMeetingHistoryByMentorId(long id);
        Task<List<MeetingHistoryResponse>> GetMeetingHistoryById(long id);
    }
}
