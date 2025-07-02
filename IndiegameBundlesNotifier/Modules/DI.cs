using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using IndiegameBundlesNotifier.Services;
using IndiegameBundlesNotifier.Services.Notifier;
using IndiegameBundlesNotifier.Models.Config;

namespace IndiegameBundlesNotifier.Modules {
	internal class DI {
		private static readonly IConfigurationRoot logConfig = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.Build();
		private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("Config/config.json", optional: false, reloadOnChange: true)
			.Build();

		internal static IServiceProvider BuildAll() {
			return new ServiceCollection()
			   .AddTransient<JsonOP>()
			   .AddTransient<ConfigValidator>()
			   .AddTransient<Scraper>()
			   .AddTransient<Parser>()
			   .AddTransient<NotifyOP>()
			   .AddTransient<Bark>()
			   .AddTransient<TelegramBot>()
			   .AddTransient<Email>()
			   .AddTransient<QQHttp>()
			   .AddTransient<QQWebSocket>()
			   .AddTransient<PushPlus>()
			   .AddTransient<DingTalk>()
			   .AddTransient<PushDeer>()
			   .AddTransient<Discord>()
			   .AddTransient<Meow>()
			   .Configure<Config>(configuration)
			   .AddLogging(loggingBuilder => {
				   // configure Logging with NLog
				   loggingBuilder.ClearProviders();
				   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				   loggingBuilder.AddNLog(logConfig);
			   })
			   .BuildServiceProvider();
		}
	}
}
