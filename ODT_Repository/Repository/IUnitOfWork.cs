
using ODT_Repository.Entity;
using System.Security.Cryptography;

namespace ODT_Repository.Repository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Attachment> AttachmentRepository { get; }
        public IGenericRepository<Blog> BlogRepository { get; }
        public IGenericRepository<BlogComment> BlogCommentRepository { get; }
        public IGenericRepository<BlogLike> BlogLikeRepository { get; }
        public IGenericRepository<Booking> BookingRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Conversation> ConversationRepository { get; }
        public IGenericRepository<ConversationMessage> ConversationMessageRepository { get; }
        public IGenericRepository<Major> MajorRepository { get; }
        public IGenericRepository<MeetingHistory> MeetingHistoryRepository { get; }
        public IGenericRepository<Mentor> MentorRepository { get; }
        public IGenericRepository<MentorMajor> MentorMajorRepository { get; }
        public IGenericRepository<MessageReaction> MessageReactionRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<Permission> PermissionRepository { get; }
        public IGenericRepository<Question> QuestionRepository { get; }
        public IGenericRepository<QuestionComment> QuestionCommentRepository { get; }
        public IGenericRepository<QuestionRating> QuestionRatingRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<RolePermission> RolePermissionRepository { get; }
        public IGenericRepository<Student> StudentRepository { get; }
        public IGenericRepository<StudentSubcription> StudentSubcriptionRepository { get; }
        public IGenericRepository<Subcription> SubcriptionRepository { get; }
        public IGenericRepository<Transaction> TransactionRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Wallet> WalletRepository { get; }
        public IGenericRepository<CommentImage> CommentImageRepository {  get; }
        public IGenericRepository<Token> TokenRepository { get; }
        void Save();
    }
}
