using InvoiceGenerator_dotnet_maui_UI.Services;
using InvoiceGenerator_dotnet_maui_UI.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace InvoiceGenerator_dotnet_maui_UI;

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


		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("InvoiceGenerator_dotnet_maui_UI.appsettings.json");

		var config = new ConfigurationBuilder()
					.AddJsonStream(stream)
					.Build();

		builder.Services
			.AddTransient<AppShell>()
			.AddTransient<StartPage>()
			.AddTransient<InvoiceGenerationPage>()
			.AddTransient<InvoiceGenerationViewModel>()
			.AddTransient<ClientDetailsPage>()
			.AddTransient<ClientDetailsEntryPage>()
			.AddSingleton<ClientDetailsViewPage>()
			.AddTransient<ClientDetailsViewModel>()
			.AddTransient<ClientEntryViewModel>()
			.AddTransient<ClientService>();

        return builder.Build();
	}

}
