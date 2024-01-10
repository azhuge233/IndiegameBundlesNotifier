using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Models.PostContent;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class Discord(ILogger<Discord> logger) : INotifiable {
		private readonly ILogger<Discord> _logger = logger;

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageDiscord);

				var url = config.DiscordWebhookURL;
				var content = new DiscordPostContent() {
					Content = records.Count > 1 ? "New Free Games - IndiegameBundle" : "New Free Game - IndiegameBundle"
				};

				foreach (var record in records) {
					content.Embeds.Add(
						new Embed() {
							Title = record.Title,
							Url = record.Url,
							Description = record.ToDiscordMessage(),
							Footer = new Footer() { Text = NotifyFormatStrings.projectLink }
						}
					);
				}

				var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
				var resp = await new HttpClient().PostAsync(url, data);
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDiscord}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageDiscord}");
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
