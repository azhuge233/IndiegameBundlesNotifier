﻿using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Models.WebSocketContent;
using IndiegameBundlesNotifier.Strings;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text.Json;
using Websocket.Client;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class QQWebSocket(ILogger<QQWebSocket> logger) : INotifiable {
		private readonly ILogger<QQWebSocket> _logger = logger;

		private WebsocketClient GetWSClient(NotifyConfig config) {
			var url = new Uri(string.Format(NotifyFormatStrings.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierStrings.debugWSReconnectionQQWebSocket, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSMessageRecievedQQWebSocket, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierStrings.debugWSDisconnectedQQWebSocket, msg));
			#endregion

			return client;
		}

		private static List<WSPacket> GetSendPacket(NotifyConfig config, List<FreeGameRecord> records) {
			return records.Select(record => new WSPacket() {
				Action = NotifyFormatStrings.qqWebSocketSendAction,
				Params = new Param {
					UserID = config.ToQQID,
					Message = $"{record.ToQQMessage()}{NotifyFormatStrings.projectLink}"
				}
			}).ToList();
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageQQWebSocket);

				var packets = GetSendPacket(config, records);

				using var client = GetWSClient(config);

				await client.Start();

				foreach (var packet in packets) {
					await client.SendInstant(JsonSerializer.Serialize(packet));
					await Task.Delay(600);
				}

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageQQWebSocket}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierStrings.debugSendMessageQQWebSocket}");
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
