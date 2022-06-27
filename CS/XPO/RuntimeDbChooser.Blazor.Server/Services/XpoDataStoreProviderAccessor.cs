using DevExpress.ExpressApp.Xpo;
using System.Collections.Concurrent;

namespace RuntimeDbChooser.Blazor.Server.Services;
public class XpoDataStoreProviderAccessor {
    private readonly ConcurrentDictionary<string, IXpoDataStoreProvider> xpoDataStoreProviderDictionary = new ConcurrentDictionary<string, IXpoDataStoreProvider>();
    public IXpoDataStoreProvider GetDataStoreProvider(string connectionString) {
        return xpoDataStoreProviderDictionary.GetOrAdd(connectionString, CreateDataStoreProvider);

        IXpoDataStoreProvider CreateDataStoreProvider(string connectionString) {
            //TODO Minakov - the enablePoolingInConnectionString parameter should be true.
            //The 'false' value is required only to get database name in the Updater -> ObjectSpace.Database.Contains("DB1")
            return XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, false);
        }
    }
}
