using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using DevExpress.ExpressApp.Xpo;

namespace RuntimeDbChooser.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        private readonly ConcurrentDictionary<string, IXpoDataStoreProvider> xpoDataStoreProviderDictionary = new ConcurrentDictionary<string, IXpoDataStoreProvider>();
        public IXpoDataStoreProvider GetDataStoreProvider(string connectionString, IDbConnection connection) {
            return xpoDataStoreProviderDictionary.GetOrAdd(connectionString, CreateDataStoreProvider);

            IXpoDataStoreProvider CreateDataStoreProvider(string connectionString) {
                //TODO Minakov the enablePoolingInConnectionString parameter should be true. Change after IObjectSpace.DataBase fix
                return XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, false);
            }
        }
    }
}
