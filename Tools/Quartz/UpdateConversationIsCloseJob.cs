using ODT_Repository.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Quartz;

[DisallowConcurrentExecution]
public class UpdateConversationIsCloseJob : IJob
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateConversationIsCloseJob(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            var currentTime = DateTime.Now;

            //Auto OverTime Booking 30min
            var bookings = _unitOfWork.BookingRepository
                .Get(b => b.Status == "Pending");

            foreach (var booking in bookings)
            {
                var overTimeBooking = booking.CreateAt.AddHours(0.5);
                if (overTimeBooking <= currentTime)
                {
                    booking.Status = "OverTime";
                    _unitOfWork.BookingRepository.Update(booking);
                }

            }

            //Auto End Booking & close conversation
            var closeBookings = _unitOfWork.BookingRepository
                .Get(b => b.Status == "Accepted")
                .ToList();

            foreach (var booking in closeBookings)
            {
                if (booking.EndTime <= currentTime)
                {
                    var conversation = _unitOfWork.ConversationRepository.Get(
                        c => c.User1Id == booking.UserId &&
                        c.User2Id == booking.MentorId &&
                        c.EndTime == booking.EndTime &&
                        c.IsClose == false).FirstOrDefault();

                    if (conversation != null)
                    {
                        var mentor = _unitOfWork.MentorRepository.Get(m => m.Id == conversation.User2Id).FirstOrDefault();

                        mentor.OnlineStatus = "Online";
                        booking.Status = "Ended";
                        conversation.IsClose = true;

                        _unitOfWork.MentorRepository.Update(mentor);
                        _unitOfWork.BookingRepository.Update(booking);
                        _unitOfWork.ConversationRepository.Update(conversation);
                    }
                }
            }

            //Auto Unavailable those Pending have same time with Accepted 
            var pendingBookings = _unitOfWork.BookingRepository
                    .Get(b => b.Status == "Pending")
                    .ToList();

            foreach (var pendingBooking in pendingBookings)
            {
                var overlappingAcceptedBookings = _unitOfWork.BookingRepository
                    .Get(b => b.Status == "Accepted" && b.MentorId == pendingBooking.MentorId)
                    .ToList();

                bool isOverlapping = overlappingAcceptedBookings.Any(acceptedBooking =>
                    (pendingBooking.StartTime >= acceptedBooking.StartTime && pendingBooking.StartTime < acceptedBooking.EndTime) ||
                    (pendingBooking.EndTime > acceptedBooking.StartTime && pendingBooking.EndTime <= acceptedBooking.EndTime) ||
                    (pendingBooking.StartTime <= acceptedBooking.StartTime && pendingBooking.EndTime >= acceptedBooking.EndTime)
                );

                if (isOverlapping)
                {
                    pendingBooking.Status = "Unavailable";
                    _unitOfWork.BookingRepository.Update(pendingBooking);
                }
            }

            // Auto Open Conversation
            var openConversations = _unitOfWork.ConversationRepository
                .Get(oc => oc.IsClose == true)
                .ToList();

            foreach (var conversation in openConversations)
            {
                if (conversation.CreateAt <= currentTime && currentTime <= conversation.EndTime)
                {
                    var booking = _unitOfWork.BookingRepository.Get(
                        c => c.UserId == conversation.User1Id &&
                             c.MentorId == conversation.User2Id &&
                             c.EndTime == conversation.EndTime &&
                             c.Status == "Accepted").FirstOrDefault();

                    if (booking != null)
                    {
                        var mentor = _unitOfWork.MentorRepository.Get(m => m.Id == booking.MentorId).FirstOrDefault();

                        mentor.OnlineStatus = "BeingBook";
                        conversation.IsClose = false;

                        _unitOfWork.ConversationRepository.Update(conversation);
                        _unitOfWork.MentorRepository.Update(mentor);
                    }
                }
            }

            /*var conversations = _unitOfWork.ConversationRepository
                .Get(c => c.IsClose == false)
                .ToList();

            foreach (var conversation in conversations)
            {
                var endTimeWithDuration = conversation.EndTime;
                if (endTimeWithDuration <= currentTime)
                {
                    conversation.IsClose = true;
                    _unitOfWork.ConversationRepository.Update(conversation);
                }
            }*/

            var studentSubcriptions = _unitOfWork.StudentSubcriptionRepository
                .Get(c => c.Status == true)
                .ToList();

            foreach (var studentSubcription in studentSubcriptions)
            {
                var endTimeWithDuration = studentSubcription.EndDate;
                if (endTimeWithDuration <= currentTime)
                {
                    studentSubcription.Status = false;
                    _unitOfWork.StudentSubcriptionRepository.Update(studentSubcription);
                }
            }


            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return Task.CompletedTask;
    }

}
