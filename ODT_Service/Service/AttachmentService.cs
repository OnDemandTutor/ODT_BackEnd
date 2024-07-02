using AutoMapper;
using Microsoft.AspNetCore.Http;
using ODT_Model.DTO.Response;
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
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<AttachmentResponse> GetAnotherFileAttachmentByConversationId(long conversationId)
        {
            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var conversation = _unitOfWork.ConversationRepository.GetByID(conversationId);
            if (conversation == null)
            {
                throw new CustomException.DataNotFoundException("Conversation not found.");
            }

            if (conversation.User1Id != userId)
            {
                throw new CustomException.UnauthorizedAccessException("You do not have permission to view attachments for this conversation.");
            }

            var attachment = _unitOfWork.AttachmentRepository.Get(filter: c => c.ConversationMessage.ConversationId == conversationId && !c.FileType.StartsWith("image/"));
            return _mapper.Map<List<AttachmentResponse>>(attachment);
        }

        public List<AttachmentResponse> GetImageAttachmentByConversationId(long conversationId)
        {
            //var conversationMessage = _unitOfWork.ConversationMessageRepository.GetByID(conversationMessageId);

            var userIdStr = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(userIdStr, out long userId))
            {
                throw new Exception("User ID claim invalid.");
            }

            var conversation = _unitOfWork.ConversationRepository.GetByID(conversationId);
            if (conversation == null)
            {
                throw new CustomException.DataNotFoundException("Conversation not found.");
            }

            if (conversation.User1Id != userId)
            {
                throw new CustomException.UnauthorizedAccessException("You do not have permission to view attachments for this conversation.");
            }

            var attachment = _unitOfWork.AttachmentRepository.Get(filter: c => c.ConversationMessage.ConversationId == conversationId && c.FileType.StartsWith("image/"));
            return _mapper.Map<List<AttachmentResponse>>(attachment);
        }
    }
}
