using DemoApp.Services;
using DemoApp.Services.Interfaces;
using DemoApp.ViewModels;
using DemoApp.Views;
using Microsoft.Extensions.Logging;

namespace DemoApp;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IDBService, DBService>();

        builder.Services.AddSingleton<DemoViewModel>();
        builder.Services.AddTransient<DemoView>(s => new DemoView
        {
            BindingContext = s.GetRequiredService<DemoViewModel>()
        });

        return builder.Build();
    }
}