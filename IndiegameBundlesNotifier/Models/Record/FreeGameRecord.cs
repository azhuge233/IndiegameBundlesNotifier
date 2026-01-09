using System.Text;
using IndiegameBundlesNotifier.Strings;

namespace IndiegameBundlesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Url { get; set; }

		public string Title { get; set; }

		public string ToTelegramMessage() {
			return string.Format(NotifyFormatStrings.telegramFormat, Title, Url, NotifyFormatStrings.telegramTag);
		}

		public string ToBarkMessage() {
			return string.Format(NotifyFormatStrings.barkFormat, Title, Url);
		}

		public string ToEmailMessage() {
			return string.Format(NotifyFormatStrings.emailFormat, Title, Url);
		}

		public string ToQQMessage() {
			return string.Format(NotifyFormatStrings.qqFormat, Title, Url);
		}

		public string ToPushPlusMessage() {
			return string.Format(NotifyFormatStrings.pushPlusFormat, Title, Url);
		}

		public string ToDingTalkMessage() {
			return string.Format(NotifyFormatStrings.dingTalkFormat, Title, Url);
		}

		public string ToPushDeerMessage() {
			return string.Format(NotifyFormatStrings.pushDeerFormat, Title, Url);
		}

		public string ToDiscordMessage() {
			return string.Format(NotifyFormatStrings.discordFprmat, Url);
		}

		public string ToMeowMessage() {
			return string.Format(NotifyFormatStrings.meowFormat, Title, Url);
		}
	}
}
