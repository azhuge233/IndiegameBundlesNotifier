using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;
using IndiegameBundlesNotifier.Strings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Text;

namespace IndiegameBundlesNotifier.Services.Notifier {
	internal class Email(ILogger<Email> logger): INotifiable {
		private readonly ILogger<Email> _logger = logger;

		private MimeMessage CreateMessage(List<FreeGameRecord> pushList, string fromAddress, string toAddress) {
			try {
				_logger.LogDebug(NotifierStrings.debugCreateMessage);

				var message = new MimeMessage();

				message.From.Add(new MailboxAddress("IndiegameBundles", fromAddress));
				message.To.Add(new MailboxAddress("Receiver", toAddress));

				var sb = new StringBuilder();

				message.Subject = sb.AppendFormat(NotifyFormatStrings.emailTitleFormat, pushList.Count).ToString();
				sb.Clear();

				pushList.ForEach(record => sb.AppendFormat(NotifyFormatStrings.emailBodyFormat, record.ToEmailMessage()));

				message.Body = new TextPart("html") {
					Text = sb.Append(NotifyFormatStrings.projectLinkHTML).ToString()
				};

				_logger.LogDebug($"Done: {NotifierStrings.debugCreateMessage}");
				return message;
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugCreateMessage}");
				throw;
			}
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierStrings.debugSendMessageEmail);

				var message = CreateMessage(records, config.FromEmailAddress, config.ToEmailAddress);

				using var client = new SmtpClient();
				client.Connect(config.SMTPServer, config.SMTPPort, true);
				client.Authenticate(config.AuthAccount, config.AuthPassword);
				await client.SendAsync(message);
				client.Disconnect(true);

				_logger.LogDebug($"Done: {NotifierStrings.debugSendMessageEmail}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierStrings.debugSendMessageEmail}");
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
