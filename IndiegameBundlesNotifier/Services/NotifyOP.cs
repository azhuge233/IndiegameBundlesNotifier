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

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Telegram");
					await services.GetRequiredService<TelegramBot>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Bark");
					await services.GetRequiredService<Bark>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Bark");

				//QQ notifications
				if (config.EnableQQ) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ");
					await services.GetRequiredService<QQ>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ");

				//QQ Red (Chronocat) notifications
				if (config.EnableRed) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ Red (Chronocat)");
					await services.GetRequiredService<QQRed>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ Red (Chronocat)");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushPlus");
					await services.GetRequiredService<PushPlus>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "DingTalk");
					await services.GetRequiredService<DingTalk>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushDeer");
					await services.GetRequiredService<PushDeer>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Discord");
					await services.GetRequiredService<Discord>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Discord");

				//Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Email");
					await services.GetRequiredService<Email>().SendMessage(config, pushList);
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Email");

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
