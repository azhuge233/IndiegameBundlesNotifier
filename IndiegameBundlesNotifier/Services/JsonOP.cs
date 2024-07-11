using Microsoft.Extensions.Logging;
using System.Text.Json;
using IndiegameBundlesNotifier.Strings;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Models.Config;

namespace IndiegameBundlesNotifier.Services {
	internal class JsonOP(ILogger<JsonOP> logger) : IDisposable {
		private readonly ILogger<JsonOP> _logger = logger;

		internal void WriteData(List<FreeGameRecord> data) {
			try {
				if (data.Count > 0) {
					_logger.LogDebug(JsonOPStrings.debugWrite);

					// Preserve 30 records to prevent duplications on notification
					// probably caused by unstable site content
					while (data.Count > 30) {
						_logger.LogDebug(JsonOPStrings.debugDeleteRecord, data[^1].Title);
						data.RemoveAt(data.Count - 1);
					}

					string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
					File.WriteAllText(JsonOPStrings.recordsPath, string.Empty);
					File.WriteAllText(JsonOPStrings.recordsPath, json);
					_logger.LogDebug($"Done: {JsonOPStrings.debugWrite}");
				} else _logger.LogDebug("No records detected, quit writing records");
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPStrings.debugWrite}");
				throw;
			} finally {
				Dispose();
			}
		}

		internal List<FreeGameRecord> LoadData() {
			try {
				_logger.LogDebug(JsonOPStrings.debugLoadRecords);
				var content = JsonSerializer.Deserialize<List<FreeGameRecord>>(File.ReadAllText(JsonOPStrings.recordsPath));
				_logger.LogDebug($"Done: {JsonOPStrings.debugLoadRecords}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPStrings.debugLoadRecords}");
				throw;
			}
		}

		internal Config LoadConfig() {
			try {
				_logger.LogDebug(JsonOPStrings.debugLoadConfig);
				var content = JsonSerializer.Deserialize<Config>(File.ReadAllText(JsonOPStrings.configPath));
				_logger.LogDebug($"Done: {JsonOPStrings.debugLoadConfig}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPStrings.debugLoadConfig}");
				throw;
			}
		}
		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
