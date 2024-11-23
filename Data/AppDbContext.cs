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

            // Ignore ConfirmPassword property (it should not be mapped to the database)
            modelBuilder.Entity<User>()
                .Ignore(u => u.ConfirmPassword);

            modelBuilder.Entity<InterviewRequest>()
                .HasOne(ir => ir.Candidate)
                .WithMany(u => u.InterviewRequests)
                .HasForeignKey(ir => ir.CandidateId)
                .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

            modelBuilder.Entity<Topic>()
                .HasKey(t => t.Id); // Explicitly define Id as the primary key for Topic

            modelBuilder.Entity<Topic>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200); // Example for Topic name field (if applicable)

            // Optional: Add an index on Email for faster lookups
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Ensure the Email is unique

            base.OnModelCreating(modelBuilder); // Always call base method to ensure EF Core works correctly
        }

    }
}
