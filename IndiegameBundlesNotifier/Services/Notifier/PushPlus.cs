using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.PostContent;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class PushPlus(ILogger<PushPlus> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<PushPlus> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessagePushPlus);

				var client = new HttpClient();

				var title = new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusTitleFormat, records.Count).ToString();

				var postContent = new PushPlusPostContent() {
					Token = config.PushPlusToken,
					Title = title,
					Content = CreateMessage(records)
				};

				var resp = await client.PostAsync(NotifyFormatStrings.pushPlusPostUrl, new StringContent(JsonSerializer.Serialize(postContent), Encoding.UTF8, "application/json"));
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushPlus}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessagePushPlus}");
				throw;
			} finally {
				Dispose();
			}
		}

		private string CreateMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var sb = new StringBuilder();

				records.ForEach(record => sb.AppendFormat(NotifyFormatStrings.pushPlusBodyFormat, record.ToPushPlusMessage()));

				sb.Append(NotifyFormatStrings.projectLinkHTML);

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return sb.ToString();
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
