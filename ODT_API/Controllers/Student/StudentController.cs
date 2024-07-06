using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODT_Service.Interface;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAllStudent")]
        public IActionResult GetAllStudent([FromQuery] QueryObject queryObject)
        {
            try
            {
                var students = _studentService.GetAllStudent(queryObject);
                return CustomResult("Data Load Successfully", students);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(long id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);

                return CustomResult("Data Load Successfully", student);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
