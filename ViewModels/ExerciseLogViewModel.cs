using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitnessTracker.ViewModels
{
    public class ExerciseLogViewModel
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public DateTime Date { get; set; }
        public int Reps { get; set; }
        public int? Weight { get; set; }
        public int Sets { get; set; }
        public int RPE { get; set; }
        public int? RiR { get; set; }
        [MaxLength(60)]
        public string? Notes { get; set; }
        public SelectList? Exercises { get; set; }
    }
}
