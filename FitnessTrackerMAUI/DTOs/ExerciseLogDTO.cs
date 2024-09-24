namespace FitnessTrackerAPI.DTOs
{
    public class ExerciseLogDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? RiR { get; set; }
        public int? RPE { get; set; }
        public int? Weight { get; set; }
        public string Notes { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
    }
}
