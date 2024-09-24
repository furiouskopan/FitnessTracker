using FitnessTrackerAPI.DTOs;
using FitnessTrackerMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTrackerMAUI.ViewModels
{
    public class ExerciseLogViewModel : INotifyPropertyChanged
    {
        private readonly FitnessTrackerApiService _apiService;

        // ObservableCollection for grouped exercise logs
        public ObservableCollection<GroupedExerciseLogs> ExerciseLogGroups { get; set; } = new ObservableCollection<GroupedExerciseLogs>();
        private List<ExerciseLogDTO> AllLogs { get; set; } = new List<ExerciseLogDTO>(); // Keep track of all logs for filtering

        private ExerciseLogDTO _selectedExerciseLog;
        public ExerciseLogDTO SelectedExerciseLog
        {
            get => _selectedExerciseLog;
            set
            {
                _selectedExerciseLog = value;
                OnPropertyChanged(nameof(SelectedExerciseLog));
            }
        }

        public ExerciseLogViewModel(FitnessTrackerApiService apiService)
        {
            _apiService = apiService;
            // Initialize the logs when the ViewModel is created
            Task.Run(async () => await LoadExerciseLogs());
        }

        // Load data from the API and group the logs by exercise
        public async Task LoadExerciseLogs()
        {
            var logs = await _apiService.GetExerciseLogsAsync();
            AllLogs = logs.ToList(); // Store all logs for filtering later

            var groupedLogs = AllLogs
                .GroupBy(log => log.ExerciseName)
                .Select(g => new GroupedExerciseLogs(g.Key, g.ToList()));

            ExerciseLogGroups.Clear();
            foreach (var group in groupedLogs)
            {
                ExerciseLogGroups.Add(group);
            }
        }

        // Method to filter logs based on a search string
        public void FilterLogs(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                // If the search string is empty, show all logs
                var groupedLogs = AllLogs
                    .GroupBy(log => log.ExerciseName)
                    .Select(g => new GroupedExerciseLogs(g.Key, g.ToList()));

                ExerciseLogGroups.Clear();
                foreach (var group in groupedLogs)
                {
                    ExerciseLogGroups.Add(group);
                }
            }
            else
            {
                // Filter logs based on the search string (case-insensitive)
                var filteredLogs = AllLogs
                    .Where(log => log.ExerciseName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var groupedFilteredLogs = filteredLogs
                    .GroupBy(log => log.ExerciseName)
                    .Select(g => new GroupedExerciseLogs(g.Key, g.ToList()));

                ExerciseLogGroups.Clear();
                foreach (var group in groupedFilteredLogs)
                {
                    ExerciseLogGroups.Add(group);
                }
            }
        }

        public async Task AddExerciseLog(ExerciseLogCreateDTO createDTO)
        {
            var log = await _apiService.CreateExerciseLogAsync(createDTO);
            await LoadExerciseLogs(); // Refresh after adding
        }

        public async Task UpdateExerciseLog(int id, ExerciseLogCreateDTO updateDTO)
        {
            await _apiService.UpdateExerciseLogAsync(id, updateDTO);
            await LoadExerciseLogs(); // Refresh after updating
        }

        public async Task DeleteExerciseLog(int id)
        {
            await _apiService.DeleteExerciseLogAsync(id);
            await LoadExerciseLogs(); // Refresh after deleting
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Class to represent grouped logs by exercise
    public class GroupedExerciseLogs : ObservableCollection<ExerciseLogDTO>
    {
        public string ExerciseName { get; }

        public GroupedExerciseLogs(string exerciseName, IList<ExerciseLogDTO> logs) : base(logs)
        {
            ExerciseName = exerciseName;
        }
    }
}
