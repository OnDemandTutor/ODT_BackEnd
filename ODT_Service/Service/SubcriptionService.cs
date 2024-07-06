using AutoMapper;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
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
    public class SubcriptionService : ISubcriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubcriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SubcriptionResponse>> GetAllSubcriptions(QueryObject queryObject)
        {
            Expression<Func<Subcription, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = p => p.Status == true && p.SubcriptionName.Contains(queryObject.Search);
            }

            var subcriptions = _unitOfWork.SubcriptionRepository.Get(
                filter: filter,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize);

            if (!subcriptions.Any())
            {
                throw new CustomException.DataNotFoundException("No Subcription in Database");
            }

            var subcriptionResponses = _mapper.Map<IEnumerable<SubcriptionResponse>>(subcriptions);

            return subcriptionResponses;
        }

        public async Task<Subcription> GetSubCriptionById(long id)
        {

            var subcriptionById = await _unitOfWork.SubcriptionRepository.GetByIdAsync(id);
            if (subcriptionById == null)
            {
                throw new CustomException.DataNotFoundException("ID doesn't exist");
            }
            else if (subcriptionById.Status == false)
            {
                throw new CustomException.DataNotFoundException("Data doens't exist");
            }
            return subcriptionById;
        }

        public async Task<SubcriptionResponse> CreateSubcription(CreateSubcriptionRequest subcriptionRequest)
        {
            var subcriptions = _mapper.Map<Subcription>(subcriptionRequest);

            // set trạng thái luôn true
            subcriptions.Status = true;
            await _unitOfWork.SubcriptionRepository.AddAsync(subcriptions);

            //map lại với cái response 
            SubcriptionResponse subcriptionResponse = _mapper.Map<SubcriptionResponse>(subcriptions);
            return subcriptionResponse;
        }

        public async Task<SubcriptionResponse> UpdateSubcription(long id, UpdateSubcriptionRequest updateSubcriptionRequest)
        {
            var existsubcription = _unitOfWork.SubcriptionRepository.GetByID(id);
            if (existsubcription == null)
            {
                throw new Exception("Subcription ID is not exist");
            }
            //map với cái biến đang có giá trị id
            _mapper.Map(updateSubcriptionRequest, existsubcription);

            _unitOfWork.SubcriptionRepository.Update(existsubcription);
            _unitOfWork.Save();
            var subcriptionresponse = _mapper.Map<SubcriptionResponse>(existsubcription);
            return subcriptionresponse;
        }

        public async Task<SubcriptionResponse> DeleteSubcription(long id)
        {
            var deletesubcription = _unitOfWork.SubcriptionRepository.GetByID(id);
            if (deletesubcription == null)
            {
                throw new Exception("Subcription ID is not exist");
            }

            deletesubcription.Status = false;
            _unitOfWork.SubcriptionRepository.Update(deletesubcription);
            _unitOfWork.Save();

            //map vào giá trị response
            var subcriptionresponse = _mapper.Map<SubcriptionResponse>(deletesubcription);
            return subcriptionresponse;
        }
    }
}
