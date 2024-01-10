using IndiegameBundlesNotifier.Models.Config;
using IndiegameBundlesNotifier.Models.Record;

namespace IndiegameBundlesNotifier.Services.Notifier {
	 interface INotifiable: IDisposable {
		public Task SendMessage(NotifyConfig config, List<FreeGameRecord> records);
	}
}
