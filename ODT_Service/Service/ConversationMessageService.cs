using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;
using ODT_Repository.Entity;
using ODT_Repository.Repository;
using ODT_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Firebase = Tools.Firebase;


namespace ODT_Service.Service
{
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly Tools.Firebase _firebase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ConversationMessageService(Tools.Firebase firebase, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _firebase = firebase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<ConversationMessageResponse> GetConversationMessagesByConversationId(long conversationId)
        {
            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }
            var conversation = _unitOfWork.ConversationRepository.Get(c => c.Id == conversationId).FirstOrDefault();
            if (conversation == null)
            {
                throw new CustomException.DataNotFoundException("Conversation not found.");
            }

            if(conversation.IsClose == true)
            {
                throw new CustomException.DataNotFoundException("Booking has expired.");
            }

            if (conversation.User1Id != userId && conversation.User2Id != userId)
            {
                throw new CustomException.UnauthorizedAccessException("You do not have permission to view these messages.");
            }

            var conversationMessages = _unitOfWork.ConversationMessageRepository
                .Get(filter: c => c.ConversationId == conversationId, includeProperties: "Attachments,MessageReactions");

            return _mapper.Map<List<ConversationMessageResponse>>(conversationMessages);
        }

        public async Task<bool> DeleteConversationMessage(long id)
        {
            try
            {
                var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                if (!long.TryParse(userIdStr, out long userId))
                {
                    throw new Exception("User ID claim invalid.");
                }
                var conversationMessage = _unitOfWork.ConversationMessageRepository.GetByID(id);
                if (conversationMessage == null && conversationMessage.IsDelete == true)
                {
                    throw new CustomException.DataNotFoundException("Message not found.");
                }

                var conversation = _unitOfWork.ConversationRepository.Get(c => c.Id == conversationMessage.ConversationId).FirstOrDefault();

                if (conversation == null && conversation.IsClose == true)
                {
                    throw new CustomException.DataNotFoundException("Conversation not found.");
                }

                if (conversationMessage.SenderId != userId)
                {
                    throw new CustomException.UnauthorizedAccessException("You do not have permission to delete this message.");
                }
                conversationMessage.SenderId = userId;
                conversationMessage.IsDelete = true;
                conversationMessage.DeleteAt = DateTime.Now;
                conversationMessage.Content = "Message has been deleted.";

                conversation.LastMessage = conversationMessage.Content;

                var messageReactions = _unitOfWork.MessageReactionRepository.Get(m => m.ConversationMessageId == conversationMessage.Id).ToList();
                foreach (var reaction in messageReactions)
                {
                    _unitOfWork.MessageReactionRepository.Delete(reaction);
                }

                var attachments = _unitOfWork.AttachmentRepository.Get(a => a.ConversationMessageId == conversationMessage.Id).ToList();
                foreach (var attachment in attachments)
                {
                    _unitOfWork.AttachmentRepository.Delete(attachment);
                }

                _unitOfWork.ConversationRepository.Update(conversation);
                _unitOfWork.ConversationMessageRepository.Update(conversationMessage);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ConversationMessageResponse> CreateConversationMessage(ConversationMessageRequest request)
        {
            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var conversationMessage = _mapper.Map<ConversationMessage>(request);
            conversationMessage.SenderId = userId;
            conversationMessage.IsDelete= false;
            conversationMessage.CreateTime = DateTime.Now;
            conversationMessage.IsSeen = false;

           

            var conversation = _unitOfWork.ConversationRepository.Get(c => c.Id == conversationMessage.ConversationId
                                                                 && (c.User1Id == conversationMessage.SenderId 
                                                                 || c.User2Id == conversationMessage.SenderId)).FirstOrDefault();
            if (conversation == null)
            {
                throw new CustomException.UnauthorizedAccessException("You are not in this conversation.");
            }


            if (conversation.IsClose)
            {
                throw new CustomException.InvalidDataException("Booking Time expired.");
            }

            var previousMessage = _unitOfWork.ConversationMessageRepository.Get(
                                                                           cm => cm.ConversationId == conversationMessage.ConversationId
                                                                           && cm.SenderId != conversationMessage.SenderId)
                                                                           .OrderByDescending(cm => cm.CreateTime)
                                                                           .FirstOrDefault();

            if (previousMessage != null)
            {
                previousMessage.IsSeen = true;
                _unitOfWork.ConversationMessageRepository.Update(previousMessage);
            }

            var updateLastMessage = _unitOfWork.ConversationRepository.GetByID(conversationMessage.ConversationId);

            if (updateLastMessage != null)
            {
                conversation.LastMessage = conversationMessage.Content;
                
            }

           

            _unitOfWork.ConversationMessageRepository.Insert(conversationMessage);
            _unitOfWork.Save();


            if (request.File != null)
            {
                if (request.File.Length > 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                var imageUrl = await _firebase.UploadImageAsync(request.File);

                var attachment = new Attachment
                {
                    ConversationMessageId = conversationMessage.Id,
                    FileName = request.File.FileName,
                    FileType = request.File.ContentType,
                    FileSize = request.File.Length,
                    FilePath = imageUrl,
                    CreateAt = DateTime.Now
                };

                _unitOfWork.AttachmentRepository.Insert(attachment);
                _unitOfWork.Save();
            }

            var conversationMessageResponse = _mapper.Map<ConversationMessageResponse>(conversationMessage);

            return conversationMessageResponse;
        }
    }
}
