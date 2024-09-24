using FitnessTrackerAPI.DTOs;
using FitnessTrackerMAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace FitnessTrackerMAUI.ViewModels
{
    public class AddExerciseLogViewModel : INotifyPropertyChanged
    {
        private readonly FitnessTrackerApiService _apiService;

        public ObservableCollection<ExerciseDTO> Exercises { get; set; } = new ObservableCollection<ExerciseDTO>();

        private ExerciseDTO _selectedExercise;
        public ExerciseDTO SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                _selectedExercise = value;
                OnPropertyChanged(nameof(SelectedExercise));
            }
        }
        public AddExerciseLogViewModel()
        {

        }

        public string Sets { get; set; }
        public string Reps { get; set; }
        public string Weight { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public ICommand SaveCommand { get; }

        public AddExerciseLogViewModel(FitnessTrackerApiService apiService)
        {
            _apiService = apiService;
            Task.Run(async () => await LoadExercises());

            // Initialize the save command
            SaveCommand = new Command(async () => await SaveLog());
        }

        // Load the exercises from the API
        private async Task LoadExercises()
        {
            var exercises = await _apiService.GetExercisesAsync();
            Exercises.Clear();
            foreach (var exercise in exercises)
            {
                Exercises.Add(exercise);
            }
        }

        // Save the exercise log
        private async Task SaveLog()
        {
            if (SelectedExercise == null || string.IsNullOrEmpty(Sets) || string.IsNullOrEmpty(Reps) || string.IsNullOrEmpty(Weight))
            {
                // Handle validation here
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please fill out all required fields", "OK");
                return;
            }

            var newLog = new ExerciseLogCreateDTO
            {
                ExerciseId = SelectedExercise.Id,
                Sets = int.Parse(Sets),
                Reps = int.Parse(Reps),
                Weight = int.Parse(Weight),
                Notes = Notes,
                Date = Date
            };

            await _apiService.CreateExerciseLogAsync(newLog);

            // Navigate back after saving
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
