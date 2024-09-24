using FitnessTrackerMAUI.Services;
using FitnessTrackerMAUI.ViewModels;

namespace FitnessTrackerMAUI.Views
{
    public partial class ExerciseLogPage : ContentPage
    {
        public ExerciseLogPage()
        {
            InitializeComponent();
            var apiService = new FitnessTrackerApiService(new HttpClient()); // Configure HttpClient appropriately
            BindingContext = new ExerciseLogViewModel(apiService);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = BindingContext as ExerciseLogViewModel;
            if (viewModel != null)
            {
                viewModel.FilterLogs(e.NewTextValue); // Now calling the FilterLogs method
            }
        }
    }
}
