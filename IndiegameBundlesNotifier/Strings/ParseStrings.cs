namespace IndiegameBundlesNotifier.Strings {
	internal class ParseStrings {
		#region XPath
		internal static readonly string articlesXPath = """.//div[@id='td-outer-wrap']//div[contains(@class, 'td-main-content-wrap')]//div[contains(@class, 'td-container')]//div[contains(@class, 'td_module_wrap')]//div[contains(@class, 'item-details')]""";
		internal static readonly string aLinkXPath = """.//h3[contains(@class, 'entry-title')]//a""";
		#endregion

		#region debug strings
		internal static readonly string debugParse = "Parse";

		internal static readonly string debugFoundInfo = "Found info: {0}";
		internal static readonly string debugFoundInPreviousRecord = "Found in previous records: {0}";
		internal static readonly string debugFoundNewRecord = "Found new record: {0}";
		#endregion
	}
}
