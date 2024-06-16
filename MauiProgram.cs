using Microsoft.Extensions.Logging;

namespace MemoryGameTestDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Create a builder
            var builder = MauiApp.CreateBuilder();
            
            // With the builder, create a new app
            builder
              .UseMauiApp<App>()
              .ConfigureFonts(fonts =>
              {
                  fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                  fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
              });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
