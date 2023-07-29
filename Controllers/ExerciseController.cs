using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitnessTracker.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly FitTrackerDbContext _context;

        public ExerciseController(FitTrackerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var exercises = _context.Exercises.ToList();
            return View(exercises);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ExerciseLog()
        {
            var exercises = _context.Exercises.ToList();
            var exerciseLogViewModel = new ExerciseLogViewModel
            {
                Exercises = new SelectList(exercises, "Id", "Name")
            };

            return View(exerciseLogViewModel);
        }
        public User GetCurrentUser()
        {
            // Get the user from the database using a known ID.
            // This is only for testing!
            var user = _context.Users.Find(1);
            return user;
        }

        [HttpPost]
        public IActionResult Create(Exercise exercise)
        {
            // Fetch the current user
            var currentUser = GetCurrentUser();

            // If the current user is not an admin, return forbidden
            //if (!currentUser.IsAdmin)
            //{
            //    return Forbid();
            //}
            if (ModelState.IsValid)
            {
                // Save the exercise to the database
                _context.Exercises.Add(exercise);
                _context.SaveChanges();

                // Redirect to the ExerciseLog/Create view after successful creation
                return RedirectToAction("Create", "Exercise");
            }
            // If the model is invalid, return the view with validation errors
            return View(exercise);
        }
    }
}
