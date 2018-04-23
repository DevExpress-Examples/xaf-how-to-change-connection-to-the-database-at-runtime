using System;
using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Security;
using RuntimeDbChooser.Module.Win;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Win {
	public partial class RuntimeDbChooserWindowsFormsApplication : WinApplication, IApplicationFactory {
		protected override void ReadLastLogonParametersCore(DevExpress.ExpressApp.Utils.SettingsStorage storage, object logonObject) {
			AuthenticationStandardLogonParameters standardLogonParameters = logonObject as AuthenticationStandardLogonParameters;
			if((standardLogonParameters != null) && string.IsNullOrEmpty(standardLogonParameters.UserName)) {
				base.ReadLastLogonParametersCore(storage, logonObject);
			}
		}
		protected override void OnLoggingOn(LogonEventArgs args) {
			base.OnLoggingOn(args);
			MSSqlServerChangeDatabaseHelper.UpdateDatabaseName(this, ((IDatabaseNameParameter)args.LogonParameters).DatabaseName);
		}
		protected override bool OnLogonFailed(object logonParameters, Exception e) {
			if(WinChangeDatabaseHelper.SkipLogonDialog) {
				return true;
			}
			return base.OnLogonFailed(logonParameters, e);
		}
		WinApplication IApplicationFactory.CreateApplication() {
			return CreateApplication();
		}
		public static RuntimeDbChooserWindowsFormsApplication CreateApplication() {
			RuntimeDbChooserWindowsFormsApplication winApplication = new RuntimeDbChooserWindowsFormsApplication();

			((SecurityStrategyComplex)winApplication.Security).Authentication = new WinChangeDatabaseStandardAuthentication();

			//WinChangeDatabaseActiveDirectoryAuthentication activeDirectoryAuthentication = new WinChangeDatabaseActiveDirectoryAuthentication();
			//activeDirectoryAuthentication.CreateUserAutomatically = true;
			//((SecurityStrategyComplex)winApplication.Security).Authentication = activeDirectoryAuthentication;

			if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			}
			return winApplication;
		}
	}
}
