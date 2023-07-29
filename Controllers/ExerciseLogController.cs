using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FitnessTracker.Controllers
{
    public class ExerciseLogController : Controller
    {
        private readonly FitTrackerDbContext _context;
        public ExerciseLogController(FitTrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int? exerciseId)
        {
            var exercises = _context.Exercises.ToList();

            var exerciseLogViewModel = new ExerciseLogViewModel
            {
                Exercises = new SelectList(exercises, "Id", "Name", exerciseId)
            };
            return View("~/Views/Exercise/ExerciseLog.cshtml", exerciseLogViewModel);
        }

        [HttpPost]
        public IActionResult Create(ExerciseLogViewModel exerciseLogViewModel)
        {
            if (ModelState.IsValid)
            {
                var exerciseLog = new ExerciseLog
                {
                    UserId = 1,
                    ExerciseId = exerciseLogViewModel.ExerciseId,
                    Date = exerciseLogViewModel.Date,
                    Weight = exerciseLogViewModel.Weight,
                    Reps = exerciseLogViewModel.Reps,
                    Sets = exerciseLogViewModel.Sets,
                    RPE = exerciseLogViewModel.RPE,
                    RiR = exerciseLogViewModel.RiR,
                    Notes = exerciseLogViewModel.Notes,
                };

                _context.ExerciseLogs.Add(exerciseLog);
                _context.SaveChanges();

                return Redirect("~/");
            }
            else
            {
                return RedirectToAction("Create", "Exercise");
            }
        }
        public IActionResult LogList(string searchString = "")
        {
            var exerciseLogs = _context.ExerciseLogs.Include(e => e.Exercise).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                exerciseLogs = exerciseLogs.Where(e => e.Exercise.Name.Contains(searchString));
            }
            var logs = _context.ExerciseLogs
                        .Include(log => log.Exercise)
                        .Include(log => log.User)
                        .OrderByDescending(log => log.Date)
                        .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(log => log.Exercise.Name.Contains(searchString)).ToList();
            }

            var groupedLogs = logs.GroupBy(log => log.Exercise).ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ExerciseLogPartial", groupedLogs);
            }

            return View("~/Views/Exercise/LogList.cshtml", groupedLogs);
        }
        [HttpGet]
        public ActionResult FilterLogsAjax(string searchString = "")
        {
            List<ExerciseLog> filteredLogs;

            if (string.IsNullOrEmpty(searchString))
            {
                filteredLogs = _context.ExerciseLogs.Include(e => e.Exercise).ToList();
            }
            else
            {
                filteredLogs = _context.ExerciseLogs.Include(e => e.Exercise)
                                     .Where(e => EF.Functions.Like(e.Exercise.Name, searchString + "%"))
                                     .ToList();
            }

            var groupedLogs = filteredLogs.GroupBy(log => log.Exercise).ToList();

            return PartialView("_ExerciseLogPartial", groupedLogs);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var exerciseLog = _context.ExerciseLogs.FirstOrDefault(el => el.Id == id);
            if (exerciseLog == null)
            {
                return NotFound();
            }

            var exercises = _context.Exercises.ToList();

            var exerciseLogViewModel = new ExerciseLogViewModel
            {
                Id = exerciseLog.Id,
                ExerciseId = exerciseLog.ExerciseId,
                ExerciseName = exerciseLog.Exercise.Name,
                Date = exerciseLog.Date,
                Weight = exerciseLog.Weight,
                Reps = exerciseLog.Reps,
                Sets = exerciseLog.Sets,
                RPE = exerciseLog.RPE,
                RiR = exerciseLog.RiR,
                Notes = exerciseLog.Notes,
                Exercises = new SelectList(exercises, "Id", "Name")
            };
            return View(exerciseLogViewModel);
        }

        [HttpPost]
        public IActionResult Update(ExerciseLogViewModel exerciseLogViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the exercise with the provided ExerciseId exists
                bool exerciseExists = _context.Exercises.Any(e => e.Id == exerciseLogViewModel.ExerciseId);
                if (!exerciseExists)
                {
                    // ExerciseId does not exist, handle the error (e.g., show a validation message)
                    ModelState.AddModelError(string.Empty, "Invalid ExerciseId");
                    return View(exerciseLogViewModel);
                }

                var exerciseLog = _context.ExerciseLogs.FirstOrDefault(el => el.Id == exerciseLogViewModel.Id);
                if (exerciseLog == null)
                {
                    return NotFound();
                }

                exerciseLog.ExerciseId = exerciseLogViewModel.ExerciseId;
                exerciseLog.Date = exerciseLogViewModel.Date;
                exerciseLog.Weight = exerciseLogViewModel.Weight;
                exerciseLog.Reps = exerciseLogViewModel.Reps;
                exerciseLog.Sets = exerciseLogViewModel.Sets;
                exerciseLog.RPE = exerciseLogViewModel.RPE;
                exerciseLog.RiR = exerciseLogViewModel.RiR;
                exerciseLog.Notes = exerciseLogViewModel.Notes;

                _context.ExerciseLogs.Update(exerciseLog);
                _context.SaveChanges();

                return RedirectToAction("LogList", "ExerciseLog");
            }
            return View(exerciseLogViewModel);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var exerciseLog = _context.ExerciseLogs.FirstOrDefault(el => el.Id == id);
            if (exerciseLog == null)
            {
                return NotFound();
            }

            _context.ExerciseLogs.Remove(exerciseLog);
            _context.SaveChanges();

            return Ok();
        }
    }
}