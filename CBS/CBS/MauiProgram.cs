using CBS.Infrastructure;
using CBS.Services;
using CBS.Shared.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace CBS
{
    public static class MauiProgram
    {
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=CbsDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMudServices();
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddInfrastructure(ConnectionString);

            builder.Services.AddSingleton<AdminStateService>();
            builder.Services.AddSingleton<VendorStateService>();
            builder.Services.AddSingleton<ContributionStateService>();
            builder.Services.AddSingleton<ExpenseStateService>();
            builder.Services.AddSingleton<MemberStateService>();
            builder.Services.AddSingleton<NotificationStateService>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            app.Services.InitializeDatabase();
            return app;
        }
    }
}
