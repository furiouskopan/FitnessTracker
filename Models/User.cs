namespace FitnessTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        // Navigation properties
        public ICollection<ExerciseLog> ExerciseLogs { get; set; }
    }
}
