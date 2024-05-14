using Microsoft.EntityFrameworkCore;
using ODT_Repository.Entity;

namespace ODT_Repository.Entity
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogsComments { get; set; }
        public DbSet<BlogLike> BlogsLikes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionComment> QuestionComments { get; set; }
        public DbSet<QuestionRating> QuestionRatings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubcription> StudentSubcriptions { get; set; }
        public DbSet<Subcription> Subcriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.roleId, rp.permissionId });
            modelBuilder.Entity<StudentSubcription>()
                .HasKey(ss => new { ss.studentId, ss.subcriptionId });
        }
    }
}
