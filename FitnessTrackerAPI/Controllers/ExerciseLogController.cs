// Controllers/ExerciseLogController.cs
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.DTOs;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ExerciseLogController : ControllerBase
    {
        private readonly FitTrackerDbContext _context;

        public ExerciseLogController(FitTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/ExerciseLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseLogDTO>>> GetExerciseLogs()
        {
            var userId = "1"; // Hardcoded integer user ID for testing

            var logs = await _context.ExerciseLogs
                .Include(el => el.Exercise)
                .Where(el => el.UserId == userId)
                .OrderByDescending(el => el.Date)
                .ToListAsync();


            var logDTOs = logs.Select(el => new ExerciseLogDTO
            {
                Id = el.Id,
                Date = el.Date,
                Sets = el.Sets,
                Reps = el.Reps,
                RiR = el.RiR,
                RPE = el.RPE,
                Weight = el.Weight,
                Notes = el.Notes,
                ExerciseId = el.ExerciseId,
                ExerciseName = el.Exercise.Name
            });

            return Ok(logDTOs);
        }

        // GET: api/ExerciseLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseLogDTO>> GetExerciseLog(int id)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = "1";

            var log = await _context.ExerciseLogs
                .Include(el => el.Exercise)
                .FirstOrDefaultAsync(el => el.Id == id && el.UserId == userId);

            if (log == null)
            {
                return NotFound();
            }

            var logDTO = new ExerciseLogDTO
            {
                Id = log.Id,
                Date = log.Date,
                Sets = log.Sets,
                Reps = log.Reps,
                RiR = log.RiR,
                RPE = log.RPE,
                Weight = log.Weight,
                Notes = log.Notes,
                ExerciseId = log.ExerciseId,
                ExerciseName = log.Exercise.Name
            };

            return Ok(logDTO);
        }

        // POST: api/ExerciseLog
        [HttpPost]
        public async Task<ActionResult<ExerciseLogDTO>> CreateExerciseLog(ExerciseLogCreateDTO createDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var exerciseLog = new ExerciseLog
            {
                //UserId = userId,
                UserId = "1",
                Date = createDTO.Date,
                Sets = createDTO.Sets,
                Reps = createDTO.Reps,
                RiR = createDTO.RiR,
                RPE = createDTO.RPE,
                Weight = createDTO.Weight,
                Notes = createDTO.Notes,
                ExerciseId = createDTO.ExerciseId
            };

            _context.ExerciseLogs.Add(exerciseLog);
            await _context.SaveChangesAsync();

            var logDTO = new ExerciseLogDTO
            {
                Id = exerciseLog.Id,
                Date = exerciseLog.Date,
                Sets = exerciseLog.Sets,
                Reps = exerciseLog.Reps,
                RiR = exerciseLog.RiR,
                RPE = exerciseLog.RPE,
                Weight = exerciseLog.Weight,
                Notes = exerciseLog.Notes,
                ExerciseId = exerciseLog.ExerciseId,
                ExerciseName = (await _context.Exercises.FindAsync(exerciseLog.ExerciseId)).Name
            };

            return CreatedAtAction(nameof(GetExerciseLog), new { id = exerciseLog.Id }, logDTO);
        }

        //// PUT: api/ExerciseLog/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateExerciseLog(int id, ExerciseLogCreateDTO updateDTO)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var exerciseLog = await _context.ExerciseLogs.FindAsync(id);
        //    if (exerciseLog == null || exerciseLog.UserId != userId)
        //    {
        //        return NotFound();
        //    }

        //    exerciseLog.Date = updateDTO.Date;
        //    exerciseLog.Sets = updateDTO.Sets;
        //    exerciseLog.Reps = updateDTO.Reps;
        //    exerciseLog.RiR = updateDTO.RiR;
        //    exerciseLog.RPE = updateDTO.RPE;
        //    exerciseLog.Weight = updateDTO.Weight;
        //    exerciseLog.Notes = updateDTO.Notes;
        //    exerciseLog.ExerciseId = updateDTO.ExerciseId;

        //    _context.Entry(exerciseLog).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //// DELETE: api/ExerciseLog/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteExerciseLog(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var exerciseLog = await _context.ExerciseLogs.FindAsync(id);
        //    if (exerciseLog == null || exerciseLog.UserId != userId)
        //    {
        //        return NotFound();
        //    }

        //    _context.ExerciseLogs.Remove(exerciseLog);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
