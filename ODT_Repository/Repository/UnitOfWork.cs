
using ODT_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _context = new MyDbContext();
        private IGenericRepository<Attachment> _attachmentRepository;
        private IGenericRepository<Blog> _blogRepository;
        private IGenericRepository<BlogComment> _blogCommentRepository;
        private IGenericRepository<BlogLike> _blogLikeRepository;
        private IGenericRepository<Booking> _bookingRepository;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Conversation> _conversationRepository;
        private IGenericRepository<ConversationMessage> _conversationMessageRepository;
        private IGenericRepository<Major> _majorRepository;
        private IGenericRepository<MeetingHistory> _meetingHistoryRepository;
        private IGenericRepository<Mentor> _mentorRepository;
        private IGenericRepository<MentorMajor> _mentorMajorRepository;
        private IGenericRepository<MessageReaction> _messageReactionRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<Permission> _permissionRepository;
        private IGenericRepository<Question> _questionRepository;
        private IGenericRepository<QuestionComment> _questionCommentRepository;
        private IGenericRepository<QuestionRating> _questionRatingRepository;
        private IGenericRepository<Role> _roleRepository;
        private IGenericRepository<RolePermission> _rolePermissionRepository;
        private IGenericRepository<Student> _studentRepository;
        private IGenericRepository<StudentSubcription> _StudentSubcriptionRepository;
        private IGenericRepository<Subcription> _subcriptionRepository;
        private IGenericRepository<Transaction> _transactionRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Wallet> _walletRepository;
        private IGenericRepository<CommentImage> _commentImage;
        private IGenericRepository<Token> _tokenRepository;

        public UnitOfWork()
        {
        }
        public IGenericRepository<Attachment> AttachmentRepository
        {
            get
            {

                if (_attachmentRepository == null)
                {
                    _attachmentRepository = new GenericRepository<Attachment>(_context);
                }
                return _attachmentRepository;
            }
        }
        public IGenericRepository<Blog> BlogRepository
        {
            get
            {

                if (_blogRepository == null)
                {
                    _blogRepository = new GenericRepository<Blog>(_context);
                }
                return _blogRepository;
            }
        }
        public IGenericRepository<BlogComment> BlogCommentRepository
        {
            get
            {

                if (_blogCommentRepository == null)
                {
                    _blogCommentRepository = new GenericRepository<BlogComment>(_context);
                }
                return _blogCommentRepository;
            }
        }
        public IGenericRepository<BlogLike> BlogLikeRepository
        {
            get
            {

                if (_blogLikeRepository == null)
                {
                    _blogLikeRepository = new GenericRepository<BlogLike>(_context);
                }
                return _blogLikeRepository;
            }
        }
        public IGenericRepository<Booking> BookingRepository
        {
            get
            {

                if (_bookingRepository == null)
                {
                    _bookingRepository = new GenericRepository<Booking>(_context);
                }
                return _bookingRepository;
            }
        }
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {

                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }
        public IGenericRepository<Conversation> ConversationRepository
        {
            get
            {

                if (_conversationRepository == null)
                {
                    _conversationRepository = new GenericRepository<Conversation>(_context);
                }
                return _conversationRepository;
            }
        }
        public IGenericRepository<ConversationMessage> ConversationMessageRepository
        {
            get
            {

                if (_conversationMessageRepository == null)
                {
                    _conversationMessageRepository = new GenericRepository<ConversationMessage>(_context);
                }
                return _conversationMessageRepository;
            }
        }
        public IGenericRepository<Major> MajorRepository
        {
            get
            {

                if (_majorRepository == null)
                {
                    _majorRepository = new GenericRepository<Major>(_context);
                }
                return _majorRepository;
            }
        }
        public IGenericRepository<MeetingHistory> MeetingHistoryRepository
        {
            get
            {

                if (_meetingHistoryRepository == null)
                {
                    _meetingHistoryRepository = new GenericRepository<MeetingHistory>(_context);
                }
                return _meetingHistoryRepository;
            }
        }
        public IGenericRepository<Mentor> MentorRepository
        {
            get
            {

                if (_mentorRepository == null)
                {
                    _mentorRepository = new GenericRepository<Mentor>(_context);
                }
                return _mentorRepository;
            }
        }
        public IGenericRepository<MentorMajor> MentorMajorRepository
        {
            get
            {

                if (_mentorMajorRepository == null)
                {
                    _mentorMajorRepository = new GenericRepository<MentorMajor>(_context);
                }
                return _mentorMajorRepository;
            }
        }
        public IGenericRepository<MessageReaction> MessageReactionRepository
        {
            get
            {

                if (_messageReactionRepository == null)
                {
                    _messageReactionRepository = new GenericRepository<MessageReaction>(_context);
                }
                return _messageReactionRepository;
            }
        }
        public IGenericRepository<Order> OrderRepository
        {
            get
            {

                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }
        public IGenericRepository<Permission> PermissionRepository
        {
            get
            {

                if (_permissionRepository == null)
                {
                    _permissionRepository = new GenericRepository<Permission>(_context);
                }
                return _permissionRepository;
            }
        }
        public IGenericRepository<Question> QuestionRepository
        {
            get
            {

                if (_questionRepository == null)
                {
                    _questionRepository = new GenericRepository<Question>(_context);
                }
                return _questionRepository;
            }
        }
        public IGenericRepository<QuestionComment> QuestionCommentRepository
        {
            get
            {

                if (_questionCommentRepository == null)
                {
                    _questionCommentRepository = new GenericRepository<QuestionComment>(_context);
                }
                return _questionCommentRepository;
            }
        }
        public IGenericRepository<QuestionRating> QuestionRatingRepository
        {
            get
            {

                if (_questionRatingRepository == null)
                {
                    _questionRatingRepository = new GenericRepository<QuestionRating>(_context);
                }
                return _questionRatingRepository;
            }
        }
        public IGenericRepository<Role> RoleRepository
        {
            get
            {

                if (_roleRepository == null)
                {
                    _roleRepository = new GenericRepository<Role>(_context);
                }
                return _roleRepository;
            }
        }
        public IGenericRepository<RolePermission> RolePermissionRepository
        {
            get
            {

                if (_rolePermissionRepository == null)
                {
                    _rolePermissionRepository = new GenericRepository<RolePermission>(_context);
                }
                return _rolePermissionRepository;
            }
        }
        public IGenericRepository<Student> StudentRepository
        {
            get
            {

                if (_studentRepository == null)
                {
                    _studentRepository = new GenericRepository<Student>(_context);
                }
                return _studentRepository;
            }
        }
        public IGenericRepository<StudentSubcription> StudentSubcriptionRepository
        {
            get
            {

                if (_StudentSubcriptionRepository == null)
                {
                    _StudentSubcriptionRepository = new GenericRepository<StudentSubcription>(_context);
                }
                return _StudentSubcriptionRepository;
            }
        }
        public IGenericRepository<Subcription> SubcriptionRepository
        {
            get
            {

                if (_subcriptionRepository == null)
                {
                    _subcriptionRepository = new GenericRepository<Subcription>(_context);
                }
                return _subcriptionRepository;
            }
        }
        public IGenericRepository<Transaction> TransactionRepository
        {
            get
            {

                if (_transactionRepository == null)
                {
                    _transactionRepository = new GenericRepository<Transaction>(_context);
                }
                return _transactionRepository;
            }
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {

                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
        public IGenericRepository<Wallet> WalletRepository
        {
            get
            {

                if (_walletRepository == null)
                {
                    _walletRepository = new GenericRepository<Wallet>(_context);
                }
                return _walletRepository;
            }
        }

        public IGenericRepository<CommentImage> CommentImageRepository
        {
            get
            {
                if(_commentImage == null)
                {
                    _commentImage = new GenericRepository<CommentImage>(_context);
                }
                return _commentImage;
            }
        }

        public IGenericRepository<Token> TokenRepository
        {
            get
            {
                if (_tokenRepository == null)
                {
                    _tokenRepository = new GenericRepository<Token>(_context);

                }

                return _tokenRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
