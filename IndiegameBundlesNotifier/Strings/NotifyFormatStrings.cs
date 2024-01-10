﻿namespace IndiegameBundlesNotifier.Strings {
	internal static class NotifyFormatStrings {
		#region ToMessage() strings
		internal static readonly string telegramFormat = "<b>IndiegameBundles</b>\n\n" +
			"<i>{0}</i>\n" +
			"文章链接: {1}\n";
		internal static readonly string barkFormat = "{0}\n" +
			"文章链接: {1}\n";
		internal static readonly string emailFormat = "<b>{0}</b><br>" +
			"文章链接: <a href=\"{1}\">{1}</a><br>";
		internal static readonly string qqFormat = "IndiegameBundles\n\n" +
			"{0}\n" +
			"文章链接: {1}\n";
		internal static readonly string pushPlusFormat = "<b>{0}</b><br>" +
			"文章链接: <a href=\"{1}\">{1}</a><br>";
		internal static readonly string dingTalkFormat = "IndiegameBundles\n\n" +
			"{0}\n" +
			"文章链接: {1}\n";
		internal static readonly string pushDeerFormat = "IndiegameBundles\n\n" +
			"{0}\n" +
			"文章链接: {1}\n";
		internal static readonly string discordFprmat = "文章链接: {0}\n";
		#endregion

		#region url, title format strings
		internal static readonly string possibleLinkFormat = "{0}\n";
		internal static readonly string possibleLinkFormatHtml = "<a href=\"{0}\">{0}</a><br>";

		internal static readonly string telegramTag = "\n#IndiegameBundles";

		internal static readonly string barkUrlFormat = "{0}/{1}/";
		internal static readonly string barkUrlTitle = "IndiegameBundles/";
		internal static readonly string barkUrlArgs = "?group=indiegamebundles" +
			"&isArchive=1" +
			"&sound=calypso" +
			"&url={0}" +
			"&copy={0}";

		internal static readonly string emailTitleFormat = "{0} new free game(s) - IndiegameBundles";
		internal static readonly string emailBodyFormat = "<br>{0}";

		internal static readonly string qqUrlFormat = "http://{0}:{1}/send_private_msg?user_id={2}&message=";
		internal static readonly string qqRedUrlFormat = "ws://{0}:{1}";
		internal static readonly string qqRedWSConnectPacketType = "meta::connect";
		internal static readonly string qqRedWSSendPacketType = "message::send";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - IndiegameBundles";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";
		internal static readonly string pushPlusPostUrl = "http://www.pushplus.plus/send";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal static readonly string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";
		#endregion

		internal static readonly string projectLink = "\n\nFrom https://github.com/azhuge233/IndiegameBundlesNotifier";
		internal static readonly string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/IndiegameBundlesNotifier\">IndiegameBundlesNotifier</a>";
	}
}
