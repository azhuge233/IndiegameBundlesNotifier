﻿using System.Text;
using IndiegameBundlesNotifier.Strings;

namespace IndiegameBundlesNotifier.Models.Record {
	public class FreeGameRecord {
		public string Url { get; set; }

		public string Title { get; set; }

		// public List<string> PossibleLinks { get; set; }

		public string ToTelegramMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.telegramFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			sb.Append(NotifyFormatStrings.telegramTag);
			return sb.ToString();
		}

		public string ToBarkMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.barkFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}

		public string ToEmailMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.emailFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormatHtml, link));
			return sb.ToString();
		}

		public string ToQQMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.qqFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}

		public string ToPushPlusMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.pushPlusFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormatHtml, link));
			return sb.ToString();
		}

		public string ToDingTalkMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.dingTalkFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}

		public string ToPushDeerMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.pushDeerFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}

		public string ToDiscordMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.discordFprmat, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}

		public string ToMeowMessage() {
			var sb = new StringBuilder().AppendFormat(NotifyFormatStrings.meowFormat, Title, Url);
			// PossibleLinks.ForEach(link => sb.AppendFormat(NotifyFormatStrings.possibleLinkFormat, link));
			return sb.ToString();
		}
	}
}
