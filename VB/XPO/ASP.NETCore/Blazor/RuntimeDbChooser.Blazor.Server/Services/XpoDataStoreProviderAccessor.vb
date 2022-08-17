'using DevExpress.ExpressApp.Xpo;
'using System.Collections.Concurrent;
'namespace RuntimeDbChooser.Blazor.Server.Services;
'public class XpoDataStoreProviderAccessor {
'    private readonly ConcurrentDictionary<string, IXpoDataStoreProvider> xpoDataStoreProviderDictionary = new ConcurrentDictionary<string, IXpoDataStoreProvider>();
'    public IXpoDataStoreProvider GetDataStoreProvider(string connectionString) {
'        return xpoDataStoreProviderDictionary.GetOrAdd(connectionString, CreateDataStoreProvider);
'        IXpoDataStoreProvider CreateDataStoreProvider(string connectionString) {
'            return XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, true);
'        }
'    }
'}
