using FitnessTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
    public class FitTrackerDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }
        public FitTrackerDbContext()
        {
        }
        public FitTrackerDbContext(DbContextOptions<FitTrackerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.Username).IsRequired();

                entity.HasMany(u => u.ExerciseLogs)
                    .WithOne(el => el.User)
                    .HasForeignKey(el => el.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ExerciseLog>(entity =>
            {
                entity.HasKey(el => el.Id);
                entity.Property(el => el.Sets).IsRequired();
                entity.Property(el => el.Reps).IsRequired();

                entity.HasOne(el => el.Exercise)
                    .WithMany()
                    .HasForeignKey(el => el.ExerciseId);
            });
        }

    }
}
