using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.PostContent;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class QQHttp(ILogger<QQHttp> logger): INotifiable {
		private readonly ILogger<QQHttp> _logger = logger;

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageQQ);

				string url = string.Format(NotifyFormatStrings.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

				var client = new HttpClient();

				var content = new QQHttpPostContent {
					UserID = config.ToQQID
				};

				var data = new StringContent(string.Empty);
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierStrings.debugSendMessageQQ} : {record.Title}");

					content.Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}";

					data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					resp = await client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQ}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageQQ}");
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
