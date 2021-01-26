#if !DXCORE3 && !NETSTANDARD
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Module {
    public class ChangeDatabaseActiveDirectoryAuthentication : AuthenticationActiveDirectory<SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication> {
        public ChangeDatabaseActiveDirectoryAuthentication() {
            CreateUserAutomatically = true;
        }
        public override bool IsLogoffEnabled {
            get {
                return true;
            }
        }
        public override bool AskLogonParametersViaUI {
            get {
                return true;
            }
        }
    }
}
#endif
