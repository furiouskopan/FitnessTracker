// Controllers/ExerciseController.cs
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.DTOs;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly FitTrackerDbContext _context;

        public ExerciseController(FitTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Exercise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            var exerciseDTOs = exercises.Select(e => new ExerciseDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                MuscleGroup = e.MuscleGroup
            });

            return Ok(exerciseDTOs);
        }

        // GET: api/Exercise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDTO>> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseDTO = new ExerciseDTO
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                MuscleGroup = exercise.MuscleGroup
            };

            return Ok(exerciseDTO);
        }

        // POST: api/Exercise
        [HttpPost]
        public async Task<ActionResult<ExerciseDTO>> CreateExercise(ExerciseCreateDTO exerciseCreateDTO)
        {
            var exercise = new Exercise
            {
                Name = exerciseCreateDTO.Name,
                Description = exerciseCreateDTO.Description,
                MuscleGroup = exerciseCreateDTO.MuscleGroup
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            var exerciseDTO = new ExerciseDTO
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                MuscleGroup = exercise.MuscleGroup
            };

            return CreatedAtAction(nameof(GetExercise), new { id = exercise.Id }, exerciseDTO);
        }

        // PUT: api/Exercise/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, ExerciseCreateDTO exerciseUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            exercise.Name = exerciseUpdateDTO.Name;
            exercise.Description = exerciseUpdateDTO.Description;
            exercise.MuscleGroup = exerciseUpdateDTO.MuscleGroup;

            _context.Entry(exercise).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Exercise/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
