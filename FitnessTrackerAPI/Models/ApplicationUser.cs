using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FitnessTrackerAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        // Navigation properties
        public virtual ICollection<ExerciseLog> ExerciseLogs { get; set; }
    }
}