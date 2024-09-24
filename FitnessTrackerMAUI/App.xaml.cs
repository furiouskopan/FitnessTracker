using FitnessTrackerMAUI.Views;

namespace FitnessTrackerMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
