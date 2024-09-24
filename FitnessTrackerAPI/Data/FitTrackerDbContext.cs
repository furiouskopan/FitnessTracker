using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FitnessTrackerAPI.Data
{
    public class FitTrackerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseLog> ExerciseLogs { get; set; }

        public FitTrackerDbContext(DbContextOptions<FitTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Name).IsRequired();
                entity.HasMany(u => u.ExerciseLogs)
                      .WithOne(el => el.User)
                      .HasForeignKey(el => el.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Exercise
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            // Configure ExerciseLog
            modelBuilder.Entity<ExerciseLog>(entity =>
            {
                entity.HasKey(el => el.Id);
                entity.Property(el => el.Sets).IsRequired();
                entity.Property(el => el.Reps).IsRequired();

                entity.HasOne(el => el.Exercise)
                      .WithMany()
                      .HasForeignKey(el => el.ExerciseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(el => el.User)
                      .WithMany(u => u.ExerciseLogs)
                      .HasForeignKey(el => el.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
