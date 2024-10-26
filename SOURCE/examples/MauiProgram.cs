using Microsoft.Extensions.Logging;
using PARCEL.Helpers;

namespace examples;

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
#if DEBUG
                DebugLogger.Log("Basic cross-platform accessibility test.");
#endif
            });
#if DEBUG
		builder.Logging.AddDebug();
#endif
        return builder.Build();
  
    }

}
