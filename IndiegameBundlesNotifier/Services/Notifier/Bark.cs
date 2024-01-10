using HtmlAgilityPack;
using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Web;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class Bark(ILogger<Bark> logger) : INotifiable {
		private readonly ILogger<Bark> _logger = logger;

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				var sb = new StringBuilder();
				string url = sb.AppendFormat(NotifyFormatStrings.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();
				var webGet = new HtmlWeb();

				foreach (var record in records) {
					sb.Clear();
					_logger.LogDebug($"{NotifierStrings.debugSendMessageBark}: {record.Title}");
					await webGet.LoadFromWebAsync(
						sb.Append(url)
						.Append(NotifyFormatStrings.barkUrlTitle)
						.Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
						.Append(HttpUtility.UrlEncode(NotifyFormatStrings.projectLink))
						.AppendFormat(NotifyFormatStrings.barkUrlArgs, HttpUtility.UrlEncode(record.Url))
						.ToString()
					);
					_logger.LogDebug(sb.ToString());
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageBark}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageBark}");
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
