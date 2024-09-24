using FitnessTrackerMAUI.Services;
using FitnessTrackerMAUI.ViewModels;

namespace FitnessTrackerMAUI.Views
{
    public partial class ExercisePage : ContentPage
    {
        public ExercisePage()
        {
            InitializeComponent();
            var apiService = new FitnessTrackerApiService(new HttpClient());
            BindingContext = new ExerciseViewModel();
        }
    }
}
