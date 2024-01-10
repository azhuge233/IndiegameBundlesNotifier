using IndiegameBundlesNotifier.Models.Record;

namespace IndiegameBundlesNotifier.Models {
	public class ParseResult {
		public List<FreeGameRecord> Records { get; set; } = [];

		public List<FreeGameRecord> NotifyRecords { get; set; } = [];
	}
}
