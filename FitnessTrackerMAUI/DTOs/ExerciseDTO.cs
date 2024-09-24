namespace FitnessTrackerAPI.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Include other properties if necessary
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
    }
}
