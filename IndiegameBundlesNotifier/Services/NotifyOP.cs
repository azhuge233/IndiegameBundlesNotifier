using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Services.Notifier;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IndiegameBundlesNotifier.Services {
	internal class NotifyOP(ILogger<NotifyOP> logger, IOptions<Config> config, TelegramBot tgBot, Bark bark, QQHttp qqHttp, QQWebSocket qqWS, PushPlus pushPlus, DingTalk dingTalk, PushDeer pushDeer, Discord discord, Email email, Meow meow) : IDisposable {
		private readonly ILogger<NotifyOP> _logger = logger;
		private readonly Config config = config.Value;

		public async Task Notify(List<FreeGameRecord> pushList) {
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
					notifyTasks.Add(tgBot.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Bark");
					notifyTasks.Add(bark.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Bark");

				//QQ Http notifications
				if (config.EnableQQHttp) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ Http");
					notifyTasks.Add(qqHttp.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ Http");

				//QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "QQ WebSocket");
					notifyTasks.Add(qqWS.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "QQ WebSocket");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushPlus");
					notifyTasks.Add(pushPlus.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "DingTalk");
					notifyTasks.Add(dingTalk.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "PushDeer");
					notifyTasks.Add(pushDeer.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Discord");
					notifyTasks.Add(discord.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Discord");

				//Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Email");
					notifyTasks.Add(email.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPStrings.debugDisabledFormat, "Email");

				// Meow notifications
				if (config.EnableMeow) {
					_logger.LogInformation(NotifyOPStrings.debugEnabledFormat, "Meow");
					notifyTasks.Add(meow.SendMessage(pushList));
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
