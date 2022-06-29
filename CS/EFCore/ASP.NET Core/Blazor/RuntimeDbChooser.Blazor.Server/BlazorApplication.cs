using System.Collections.Concurrent;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using Microsoft.Extensions.DependencyInjection;
using RuntimeDbChooser.Blazor.Server.Services;
using RuntimeDbChooser.Services;

namespace RuntimeDbChooser.Blazor.Server {
    public class RuntimeDbChooserBlazorApplication : BlazorApplication {
        private static ConcurrentDictionary<string, bool> isCompatibilityChecked = new ConcurrentDictionary<string, bool>();

        protected override bool IsCompatibilityChecked {
            get => isCompatibilityChecked.ContainsKey(ServiceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString());

            set => isCompatibilityChecked.TryAdd(ServiceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString(), value);
        }

        protected override void OnSetupStarted() {
            base.OnSetupStarted();
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
#if EASYTEST
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
#endif
        }
        protected override LogonController CreateLogonController() {
            return CreateController<Controllers.CustomLogonController>();
        }
    }
}
