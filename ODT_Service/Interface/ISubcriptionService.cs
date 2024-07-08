using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace ODT_Service.Interface
{
    public interface ISubcriptionService
    {
        Task<SubcriptionResponse> CreateSubcription(CreateSubcriptionRequest subcriptionRequest);
        Task<SubcriptionResponse> DeleteSubcription(long id);
        Task<IEnumerable<SubcriptionResponse>> GetAllSubcriptions(QueryObject queryObject);
        Task<Subcription> GetSubCriptionById(long id);
        Task<SubcriptionResponse> UpdateSubcription(long id, UpdateSubcriptionRequest updateSubcriptionRequest);
    }
}
