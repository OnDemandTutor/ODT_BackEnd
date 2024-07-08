using CoreApiResponse;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Service.Interface;
using ODT_Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace ODT_API.Controllers.Booking
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("GetAllBooking")]
        public async Task<IActionResult> GetAllBooking([FromQuery] QueryObject queryObject)
        {
            try
            {
                var bookings = await _bookingService.GetAllBooking(queryObject);
                return CustomResult("Data Load Successfully", bookings);
            }
            catch (CustomException.DataNotFoundException e)
            {
                return CustomResult(e.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllStudentBookingByHttpContext")]
        [Authorize]
        public async Task<IActionResult> GetAllStudentBookingByHttpContext()
        {
            try
            {
                var bookings = await _bookingService.GetAllStudentBookingByHttpContext();

                return CustomResult("Data Load Successfully", bookings);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllMentorBookingByHttpContext")]
        [Authorize]
        public async Task<IActionResult> GetAllMentorBookingByUserId()
        {
            try
            {
                var bookings = await _bookingService.GetAllMentorBookingByHttpContext();

                return CustomResult("Data Load Successfully", bookings);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllMentorBookingByUserId/{id}")]
        public async Task<IActionResult> GetAllMentorBookingByUserId(long id)
        {
            try
            {
                var bookings = await _bookingService.GetAllMentorBookingByUserId(id);

                return CustomResult("Data Load Successfully", bookings);
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

        [HttpGet("GetAllBookingByMentorId/{id}")]
        public async Task<IActionResult> GetAllBookingByMentorId(long id)
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingByMentorId(id);

                return CustomResult("Data Load Successfully", bookings);
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

        [HttpGet("GetAllAcceptedBookingByMentorId/{id}")]
        public async Task<IActionResult> GetAllAcceptedBookingByMentorId(long id)
        {
            try
            {
                var bookings = await _bookingService.GetAllAcceptedBookingByMentorId(id);

                return CustomResult("Data Load Successfully", bookings);
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

        [HttpPost("CreateBooking")]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                BookingResponse response = await _bookingService.CreateBooking(request);
                return CustomResult("Created Successfully", response, HttpStatusCode.OK);
            }
            catch (CustomException.DataExistException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CancelBooking/{id}")]
        [Authorize]
        public async Task<IActionResult> CancelBooking(long id)
        {
            try
            {
                var booking = await _bookingService.CancelBooking(id);
                return CustomResult("Cancel Booking Susseccful", id, HttpStatusCode.OK);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
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

        [HttpPost("AcceptBooking/{id}")]
        [Authorize]
        public async Task<IActionResult> AcceptBooking(long id)
        {
            try
            {
                var booking = await _bookingService.AcceptBooking(id);
                return CustomResult("Accept Booking Susseccful", id, HttpStatusCode.OK);
            }
            catch(CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
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

        [HttpPost("RejectBooking")]
        [Authorize]
        public async Task<IActionResult> RejectBooking(long id)
        {
            try
            {
                var booking = await _bookingService.RejectBooking(id);
                return CustomResult("Reject Booking Susseccful", id, HttpStatusCode.OK);
            }
            catch (CustomException.UnauthorizedAccessException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
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
