using AutoMapper;
using Microsoft.AspNetCore.Http;
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

namespace ODT_Service.Service
{
    public class MessageReactionService : IMessageReactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MessageReactionService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<MessageReactionResponse> GetMessageReactionByConversationMessageId(long id)
        {
            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var messageReactions = _unitOfWork.MessageReactionRepository.Get(filter: c => c.ConversationMessageId == id).ToList();

            var conversationMessage = _unitOfWork.ConversationMessageRepository.Get(cm => cm.Id == id).FirstOrDefault();
            if (conversationMessage == null || conversationMessage.IsDelete == true)
            {
                throw new CustomException.DataNotFoundException("Message not found.");
            }

            var conversation = _unitOfWork.ConversationRepository.Get(c => c.Id == conversationMessage.ConversationId).FirstOrDefault();
            if (conversation == null || conversation.IsClose == true)
            {
                throw new CustomException.DataNotFoundException("Conversation not found.");
            }

            if (conversation.User1Id != userId && conversation.User2Id != userId)
            {
                throw new CustomException.UnauthorizedAccessException("You do not have permission to view these reactions.");
            }

            return _mapper.Map<List<MessageReactionResponse>>(messageReactions);
        }


        public async Task<bool> DeleteMessageReaction(long id)
        {
            try
            {
                var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                if (!long.TryParse(userIdStr, out long userId))
                {
                    throw new Exception("User ID claim invalid.");
                }

                var messageReaction = _unitOfWork.MessageReactionRepository.GetByID(id);
                if (messageReaction == null)
                {
                    throw new CustomException.DataNotFoundException("Reaction not found.");
                }

                if (messageReaction.UserId != userId)
                {
                    throw new CustomException.UnauthorizedAccessException("You do not have permission to delete this reaction.");
                }

                var checkMessageDelete = _unitOfWork.ConversationMessageRepository.Get(cm => cm.Id == messageReaction.ConversationMessageId).FirstOrDefault();

                if(checkMessageDelete.IsDelete) {
                    throw new CustomException.DataNotFoundException("Message not found.");
                }

                _unitOfWork.MessageReactionRepository.Delete(messageReaction);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<MessageReactionResponse> CreateMessageReaction(MessageReactionRequest request)
        {
            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var messageReaction = _mapper.Map<MessageReaction>(request);

            messageReaction.UserId = userId;
            messageReaction.CreateAt = DateTime.Now;

            var conversationMessage = _unitOfWork.ConversationMessageRepository.GetByID(messageReaction.ConversationMessageId);
            if (conversationMessage == null && conversationMessage.IsDelete == true)
            {
                throw new CustomException.DataNotFoundException("Message not found.");
            }

            var conversation = _unitOfWork.ConversationRepository.Get(c => c.Id == conversationMessage.ConversationId).FirstOrDefault();
            if (conversation == null && conversation.IsClose == true)
            {
                throw new CustomException.DataNotFoundException("Conversation not found.");
            }

            if (conversation.User1Id != userId && conversation.User2Id != userId)
            {
                throw new CustomException.UnauthorizedAccessException("You do not have permission to create this reaction.");
            }

            var user = _unitOfWork.UserRepository.GetByID(userId);
            if (user == null)
            {
                throw new CustomException.DataNotFoundException("User not found.");
            }

            conversation.LastMessage = $"{user.Fullname} have clicked {messageReaction.ReactionType} on message.";
            _unitOfWork.ConversationRepository.Update(conversation);

            _unitOfWork.MessageReactionRepository.Insert(messageReaction);
            _unitOfWork.Save();

            var MessageReactionResponse = _mapper.Map<MessageReactionResponse>(messageReaction);

            return MessageReactionResponse;

        }
    }
}
