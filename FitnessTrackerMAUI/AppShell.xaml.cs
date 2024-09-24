using FitnessTrackerMAUI.Views;

namespace FitnessTrackerMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ExerciseLogPage), typeof(ExerciseLogPage));
            // Register other pages as needed
        }
    }
}
