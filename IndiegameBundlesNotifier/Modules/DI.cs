﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using IndiegameBundlesNotifier.Services;
using IndiegameBundlesNotifier.Services.Notifier;

namespace IndiegameBundlesNotifier.Modules {
	internal class DI {
		private static readonly IConfigurationRoot logConfig = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .Build();
		internal static IServiceProvider BuildDiAll() {
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
			   .AddLogging(loggingBuilder => {
				   // configure Logging with NLog
				   loggingBuilder.ClearProviders();
				   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				   loggingBuilder.AddNLog(logConfig);
			   })
			   .BuildServiceProvider();
		}

		internal static IServiceProvider BuildDiNotifierOnly() {
			return new ServiceCollection()
			   .AddTransient<TelegramBot>()
			   .AddTransient<Bark>()
			   .AddTransient<Email>()
			   .AddTransient<QQHttp>()
			   .AddTransient<QQWebSocket>()
			   .AddTransient<PushPlus>()
			   .AddTransient<DingTalk>()
			   .AddTransient<PushDeer>()
			   .AddTransient<Discord>()
			   .AddTransient<Meow>()
			   .AddLogging(loggingBuilder => {
				   // configure Logging with NLog
				   loggingBuilder.ClearProviders();
				   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				   loggingBuilder.AddNLog(logConfig);
			   })
			   .BuildServiceProvider();
		}

		internal static IServiceProvider BuildDiScraperOnly() {
			return new ServiceCollection()
			   .AddTransient<Scraper>()
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
