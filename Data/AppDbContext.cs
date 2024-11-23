using Microsoft.EntityFrameworkCore;
using MockInterviews.Models;

namespace MockInterviews.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<InterviewRequest> InterviewRequests { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and entities here

            // User entity configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Explicitly define Id as the primary key

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Ignore(u => u.ConfirmPassword); // Ignore ConfirmPassword property

            // InterviewRequest to User Relationship (CandidateId)
            modelBuilder.Entity<InterviewRequest>()
                .HasOne(ir => ir.Candidate)
                .WithMany(u => u.InterviewRequests)
                .HasForeignKey(ir => ir.CandidateId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Candidate is deleted

            // InterviewRequest to Topic Relationship (TopicId)
            modelBuilder.Entity<InterviewRequest>()
                .HasOne(ir => ir.Topic) // Ensure the Topic navigation is properly set
                .WithMany(t => t.InterviewRequests) // The reverse navigation from Topic
                .HasForeignKey(ir => ir.TopicId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Topic is deleted

            // Topic entity configuration
            modelBuilder.Entity<Topic>()
                .HasKey(t => t.Id); // Explicitly define Id as the primary key for Topic

            modelBuilder.Entity<Topic>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200); // Example for Topic name field (if applicable)

            // Ensure Email is unique for User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Ensure the Email is unique

            base.OnModelCreating(modelBuilder); // Always call base method to ensure EF Core works correctly
        }
    }
}
