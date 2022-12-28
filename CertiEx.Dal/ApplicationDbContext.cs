using CertiEx.Domain.Exam;
using CertiEx.Domain.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CertiEx.Dal
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Choice> Choice { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Result> Result { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizAttempt>(eb =>
            {
                eb.HasNoKey();
                eb.ToView(null);
            });

            modelBuilder.Entity<QuizReport>(eb =>
            {
                eb.HasNoKey();
                eb.ToView(null);
            });
        }
    }
}