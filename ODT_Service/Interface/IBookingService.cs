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
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponse>> GetAllBooking(QueryObject queryObject);
        Task<List<BookingResponse>> GetAllStudentBookingByHttpContext();
        Task<List<BookingResponse>> GetAllMentorBookingByHttpContext();
        Task<List<BookingResponse>> GetAllMentorBookingByUserId(long id);
        Task<List<BookingResponse>> GetAllBookingByMentorId(long id);
        Task<List<BookingResponse>> GetAllAcceptedBookingByMentorId(long id);
        Task<BookingResponse> CreateBooking(CreateBookingRequest request);
        Task<bool> CancelBooking(long id);
        Task<bool> AcceptBooking(long id);
        Task<bool> RejectBooking(long id);
    }
}
