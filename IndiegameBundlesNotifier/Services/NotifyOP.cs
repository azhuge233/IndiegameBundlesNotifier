using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using IndiegameBundlesNotifier.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IndiegameBundlesNotifier.Services.Notifier;

namespace IndiegameBundlesNotifier.Services {
	internal class NotifyOP(ILogger<NotifyOP> logger) : IDisposable {
		private readonly ILogger<NotifyOP> _logger = logger;
		private readonly IServiceProvider services = DI.BuildDiNotifierOnly();

		public async Task Notify(NotifyConfig config, List<FreeGameRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(NotifyOPStrings.debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(NotifyOPStrings.debugNotify);

				var notifyTasks = new List<Task>();

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Telegram");
					notifyTasks.Add(services.GetRequiredService<TelegramBot>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Bark");
					notifyTasks.Add(services.GetRequiredService<Bark>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Bark");

				//QQ Http notifications
				if (config.EnableQQHttp) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ Http");
					notifyTasks.Add(services.GetRequiredService<QQHttp>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ Http");

				//QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ WebSocket");
					notifyTasks.Add(services.GetRequiredService<QQWebSocket>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ WebSocket");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushPlus");
					notifyTasks.Add(services.GetRequiredService<PushPlus>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "DingTalk");
					notifyTasks.Add(services.GetRequiredService<DingTalk>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushDeer");
					notifyTasks.Add(services.GetRequiredService<PushDeer>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Discord");
					notifyTasks.Add(services.GetRequiredService<Discord>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Discord");

				//Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Email");
					notifyTasks.Add(services.GetRequiredService<Email>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Email");

				// Meow notifications
				if (config.EnableMeow) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Meow");
					notifyTasks.Add(services.GetRequiredService<Meow>().SendMessage(config, pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Meow");

				await Task.WhenAll(notifyTasks);

				_logger.LogDebug($"Done: {NotifyOPStrings.debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifyOPStrings.debugNotify}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
