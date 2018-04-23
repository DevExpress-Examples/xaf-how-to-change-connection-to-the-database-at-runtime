using System.Web;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Module.Web {
	public class WebChangeDatabaseAuthenticationActiveDirectory : AuthenticationActiveDirectory<SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication> {
		public WebChangeDatabaseAuthenticationActiveDirectory() {
			this.CreateUserAutomatically = true;
		}
		public override bool IsLogoffEnabled {
			get {
				return true;
			}
		}
		public override bool AskLogonParametersViaUI {
			get {
				string databaseName = HttpContext.Current.Request.Params[WebChangeDatabaseController.DatabaseParameterName];
				if(string.IsNullOrEmpty(databaseName)) {
					return true;
				}
				return base.AskLogonParametersViaUI;
			}
		}
	}
}
