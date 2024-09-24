using Microsoft.Extensions.Logging;

namespace FitnessTrackerMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register HttpClient for API access
            builder.Services.AddHttpClient("FitnessTrackerAPI", client =>
            {
                client.BaseAddress = new Uri("https://your-api-url.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
