using Microsoft.Extensions.Logging;
using IndiegameBundlesNotifier.Strings;

namespace IndiegameBundlesNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger) : IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		internal async Task<string> GetSource() {
			try {
				InitHttpClient();

				_logger.LogDebug(ScrapeStrings.debugGetSource);

				var resp = await Client.GetAsync(ScrapeStrings.Url);
				var content = await resp.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeStrings.debugGetSource}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeStrings.debugGetSource}");
				throw;
			} finally {
				Dispose();
			}
		}

		private void InitHttpClient() {
			Client.DefaultRequestHeaders.Add("User-Agent", ScrapeStrings.UAs[new Random().Next(0, ScrapeStrings.UAs.Length - 1)]);
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
