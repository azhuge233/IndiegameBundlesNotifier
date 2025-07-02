using IndiegameBundlesNotifier.Models.Record;

namespace IndiegameBundlesNotifier.Services.Notifier {
	 interface INotifiable: IDisposable {
		public Task SendMessage(List<FreeGameRecord> records);
	}
}
