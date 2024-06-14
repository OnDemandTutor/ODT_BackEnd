using CoreApiResponse;
using FuStudy_Model.DTO.Request;
using FuStudy_Model.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using ODT_Service.Interface;
using System.Net;

namespace ODT_API.Controllers.Subcription
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcriptionControllers : BaseController
    {
        private readonly ISubcriptionService _subcriptionService;

        public SubcriptionControllers(ISubcriptionService subcriptionService)
        {
            _subcriptionService = subcriptionService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllSubcriptions()
        {
            try
            {
                var subcriptions = await _subcriptionService.GetAllSubcriptions();
                return CustomResult("Get Subcription Success", subcriptions, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCriptionById(long id)
        {
            try
            {
                var subcriptions = await _subcriptionService.GetSubCriptionById(id);
                //if (subcriptions == null)
                //{
                //    return CustomResult("Id is not exist", subcriptions, HttpStatusCode.NotFound);
                //}
                return CustomResult("ID found: ", subcriptions, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubcription([FromBody] CreateSubcriptionRequest subcriptionRequest)
        {
            try
            {
                SubcriptionResponse subcription = await _subcriptionService.CreateSubcription(subcriptionRequest);
                return CustomResult("Created Successful", subcription, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSubcription(long id, [FromBody] UpdateSubcriptionRequest updateSubcriptionRequest)
        {
            try
            {
                SubcriptionResponse subcription = await _subcriptionService.UpdateSubcription(id, updateSubcriptionRequest);
                return CustomResult("updated Successfull", subcription, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcription(long id)
        {
            try
            {
                var deletesubcription = await _subcriptionService.DeleteSubcription(id);
                return CustomResult("Delete Successfull (Status)", deletesubcription, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

