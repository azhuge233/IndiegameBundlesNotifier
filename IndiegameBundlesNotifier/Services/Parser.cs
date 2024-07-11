using HtmlAgilityPack;
using IndiegameBundlesNotifier.Models;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;

namespace IndiegameBundlesNotifier.Services {
	internal class Parser(ILogger<Parser> logger): IDisposable {
		#region DI
		private readonly ILogger<Parser> _logger = logger;
		// private readonly IServiceProvider services = DI.BuildDiScraperOnly();
		#endregion

		public ParseResult Parse(string source, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug("Start parsing");
				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(source);

				var parseResult = new ParseResult();

				var articles = htmlDoc.DocumentNode.SelectNodes(ParseStrings.articlesXPath);

				if (articles != null) {
					foreach (var each in articles) {
						var aLink = each.SelectSingleNode(ParseStrings.aLinkXPath);

						var title = aLink.Attributes["title"].Value;
						var link = aLink.Attributes["href"].Value;

						_logger.LogInformation(ParseStrings.debugFoundInfo, title);

						var newRecord = new FreeGameRecord() {
							Title = title,
							Url = link
						};

						if (records.Count == 0 || !records.Any(record => record.Url == newRecord.Url)) {
							_logger.LogInformation(ParseStrings.debugFoundNewRecord, title);
							parseResult.Records.Add(newRecord);
							parseResult.NotifyRecords.Add(newRecord);
						} else _logger.LogDebug(ParseStrings.debugFoundInPreviousRecord, title);
					}

					// Add old records back AFTER new records
					// Must be placed here since old records must be recorded after new records
					// so when JsonOP deletes, this makes sure it deletes old ones instead of new ones
					parseResult.Records.AddRange(records);
				} else _logger.LogDebug("No articles detected");

				_logger.LogDebug("Done");
				return parseResult;
			} catch (Exception) {
				_logger.LogError("Parsing Error");
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
