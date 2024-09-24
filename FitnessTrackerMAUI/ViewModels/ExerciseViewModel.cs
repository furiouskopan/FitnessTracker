using FitnessTrackerAPI.DTOs;
using FitnessTrackerMAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessTrackerMAUI.ViewModels
{
    public class ExerciseViewModel : INotifyPropertyChanged
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

        public ExerciseViewModel()
        {
            _apiService = new FitnessTrackerApiService(new HttpClient());
            LoadExercisesCommand = new Command(async () => await LoadExercisesAsync());
            AddExerciseCommand = new Command(async () => await AddExerciseAsync());
            UpdateExerciseCommand = new Command<ExerciseCreateDTO>(async (updateDTO) => await UpdateExerciseAsync(SelectedExercise?.Id ?? 0, updateDTO));
            DeleteExerciseCommand = new Command(async () => await DeleteExerciseAsync(SelectedExercise?.Id ?? 0));

            // Load exercises when ViewModel is created
            Task.Run(async () => await LoadExercisesAsync());
        }

        // Commands for binding to UI
        public ICommand LoadExercisesCommand { get; }
        public ICommand AddExerciseCommand { get; }
        public ICommand UpdateExerciseCommand { get; }
        public ICommand DeleteExerciseCommand { get; }

        public string NewExerciseName { get; set; }
        public string NewMuscleGroup { get; set; }
        public string NewExerciseDescription { get; set; }

        // Load exercises from the API
        private async Task LoadExercisesAsync()
        {
            try
            {
                var exercises = await _apiService.GetExercisesAsync();
                Exercises.Clear(); // Clear the list before adding new items
                foreach (var exercise in exercises)
                {
                    if (!Exercises.Any(e => e.Id == exercise.Id))
                    {
                        Exercises.Add(exercise);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading exercises: {ex.Message}");
            }
        }

        // Add a new exercise
        private async Task AddExerciseAsync()
        {
            var createDTO = new ExerciseCreateDTO
            {
                Name = NewExerciseName,
                MuscleGroup = NewMuscleGroup,
                Description = NewExerciseDescription
            };
            await _apiService.CreateExerciseAsync(createDTO);
            await LoadExercisesAsync(); // Refresh the exercise list after adding
        }

        // Update selected exercise
        private async Task UpdateExerciseAsync(int id, ExerciseCreateDTO updateDTO)
        {
            if (id == 0) return;

            var isSuccess = await _apiService.UpdateExerciseAsync(id, updateDTO);
            if (isSuccess)
            {
                var exerciseToUpdate = Exercises.FirstOrDefault(ex => ex.Id == id);
                if (exerciseToUpdate != null)
                {
                    exerciseToUpdate.Name = updateDTO.Name;
                    exerciseToUpdate.Description = updateDTO.Description;
                    exerciseToUpdate.MuscleGroup = updateDTO.MuscleGroup;
                    OnPropertyChanged(nameof(Exercises));
                }
            }
        }

        // Delete selected exercise
        private async Task DeleteExerciseAsync(int id)
        {
            if (id == 0) return;

            var isDeleted = await _apiService.DeleteExerciseAsync(id);
            if (isDeleted)
            {
                var exerciseToRemove = Exercises.FirstOrDefault(ex => ex.Id == id);
                if (exerciseToRemove != null)
                {
                    Exercises.Remove(exerciseToRemove);
                }
            }
        }

        // PropertyChanged event handler for INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
