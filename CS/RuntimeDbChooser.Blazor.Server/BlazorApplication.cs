using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.ExpressApp.Xpo;
using RuntimeDbChooser.Blazor.Server.Services;
using RuntimeDbChooser.Module.BusinessObjects;
using System.Collections.Concurrent;

namespace RuntimeDbChooser.Blazor.Server {
    public partial class RuntimeDbChooserBlazorApplication : BlazorApplication {
        private static ConcurrentDictionary<string, bool> isCompatibilityChecked = new ConcurrentDictionary<string, bool>();

        public RuntimeDbChooserBlazorApplication() {
            InitializeComponent();
        }

        protected override bool IsCompatibilityChecked {
            get {
                return isCompatibilityChecked.GetOrAdd(ConnectionString, false);
            }

            set {
                isCompatibilityChecked.TryAdd(ConnectionString, value);
            }
        }

        protected override void OnSetupStarted() {
            base.OnSetupStarted();
            IConfiguration configuration = ServiceProvider.GetRequiredService<IConfiguration>();
            if(configuration.GetConnectionString("ConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("ConnectionString");
            }
#if EASYTEST
            if(configuration.GetConnectionString("EasyTestConnectionString") != null) {
                ConnectionString = configuration.GetConnectionString("EasyTestConnectionString");
            }
#endif
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            IXpoDataStoreProvider dataStoreProvider = GetDataStoreProvider(args.ConnectionString, args.Connection);
            args.ObjectSpaceProviders.Add(new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)Security, dataStoreProvider, true));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }

        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            return ServiceProvider.GetRequiredService<XpoDataStoreProviderAccessor>().GetDataStoreProvider(connectionString, connection);
        }

        protected override void OnLoggingOn(LogonEventArgs args) {
            base.OnLoggingOn(args);
            string targetDataBaseName = ((IDatabaseNameParameter)args.LogonParameters).DatabaseName;
            ((XPObjectSpaceProvider)ObjectSpaceProviders[0]).SetDataStoreProvider(
                GetDataStoreProvider(MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, ConnectionString),
                null));
        }
        private void RuntimeDbChooserBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
