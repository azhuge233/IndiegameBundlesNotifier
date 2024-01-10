using HtmlAgilityPack;
using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Web;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class PushDeer(ILogger<PushDeer> logger): INotifiable {
		private readonly ILogger<PushDeer> _logger = logger;

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessagePushDeer);
				var sb = new StringBuilder();
				var webGet = new HtmlWeb();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierStrings.debugSendMessagePushDeer} : {record.Title}");
					await webGet.LoadFromWebAsync(
						new StringBuilder()
						.AppendFormat(NotifyFormatStrings.pushDeerUrlFormat,
									config.PushDeerToken,
									HttpUtility.UrlEncode(record.ToPushDeerMessage()))
						.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
						.ToString()
					);
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessagePushDeer}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessagePushDeer}");
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
