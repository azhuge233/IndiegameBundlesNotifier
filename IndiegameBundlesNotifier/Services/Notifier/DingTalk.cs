﻿using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.PostContent;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class DingTalk(ILogger<DingTalk> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<DingTalk> _logger = logger;
		private readonly Config config = config.Value;

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageDingTalk);

				var url = new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkUrlFormat, config.DingTalkBotToken).ToString();
				var content = new DingTalkPostContent();

				foreach (var record in records) {
					content.Text.Content_ = $"{record.ToDingTalkMessage()}{NotifyFormatStrings.projectLink}";
					var data = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
					var resp = await new HttpClient().PostAsync(url, data);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageDingTalk}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageDingTalk}");
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
