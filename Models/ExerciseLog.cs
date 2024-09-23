using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class ExerciseLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? RiR { get; set; }
        public int? RPE { get; set; }
        public int? Weight { get; set; }
        [MaxLength(60)]
        public string? Notes { get; set; }

        // Foreign key properties
        public string UserId { get; set; }
        public int ExerciseId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Exercise Exercise { get; set; }
    }
}
