using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.PostContent;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class Meow(ILogger<Meow> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<Meow> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageMeow);

				var url = string.Format(NotifyFormatStrings.meowUrlFormat, config.MeowAddress, config.MeowNickname);

				var content = new MeowPostContent() { 
					Title = NotifyFormatStrings.meowUrlTitle
				};

				var client = new HttpClient();

				foreach (var record in records) {
					content.Message = record.ToMeowMessage();
					content.Url = record.Url;

					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await new HttpClient().PostAsync(url, data);
					
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					await Task.Delay(3000); // rate limit
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageMeow}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageMeow}");
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
