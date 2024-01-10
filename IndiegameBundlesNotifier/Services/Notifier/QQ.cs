using HtmlAgilityPack;
using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Web;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class QQ(ILogger<QQ> logger): INotifiable {
		private readonly ILogger<QQ> _logger = logger;

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageQQ);

				string url = new StringBuilder().AppendFormat(NotifyFormatStrings.qqUrlFormat, config.QQAddress, config.QQPort, config.ToQQID).ToString();
				var sb = new StringBuilder();
				var webGet = new HtmlWeb();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierStrings.debugSendMessageQQ} : {record.Title}");
					var res = await webGet.LoadFromWebAsync(
						new StringBuilder()
							.Append(url)
							.Append(HttpUtility.UrlEncode(record.ToQQMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
							.ToString()
					);
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
