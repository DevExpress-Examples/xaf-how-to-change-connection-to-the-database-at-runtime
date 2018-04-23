using System;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using RuntimeDbChooser.Module.BusinessObjects;
using DevExpress.ExpressApp.Security.Strategy;

namespace RuntimeDbChooser.Module.Win {
	public class WinChangeDatabaseHelper {
		private static bool skipLogonDialog = false;
		public static string DatabaseName;
		public static bool AuthenticatedUserLogonFailed = false;
		public static bool SkipLogonDialog {
			get { return skipLogonDialog; }
			set { skipLogonDialog = value; }
		}
	}

	public class WinChangeDatabaseStandardAuthentication : AuthenticationStandard<SecuritySystemUser, CustomLogonParametersForStandardAuthentication> {
		public static string AuthenticatedUserName;

		public override bool AskLogonParametersViaUI {
			get {
				if(WinChangeDatabaseHelper.SkipLogonDialog) {
					return false;
				}
				return base.AskLogonParametersViaUI;
			}
		}
		public override object Authenticate(DevExpress.ExpressApp.IObjectSpace objectSpace) {
			WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = false;
			if(string.IsNullOrEmpty(AuthenticatedUserName)) {
				return base.Authenticate(objectSpace);
			}
			else {
				CustomLogonParametersForStandardAuthentication logonParameters = (CustomLogonParametersForStandardAuthentication)LogonParameters;
				object result = objectSpace.FindObject(UserType, new BinaryOperator("UserName", logonParameters.UserName));
				if(result == null) {
					WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = true;
					WinChangeDatabaseHelper.SkipLogonDialog = false;
					throw new AuthenticationException(logonParameters.UserName, SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation));
				}
				AuthenticatedUserName = "";
				return result;
			}
		}
	}

	public class WinChangeDatabaseActiveDirectoryAuthentication : AuthenticationActiveDirectory<SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication> {
		public override bool IsLogoffEnabled {
			get {
				return true;
			}
		}
		public override bool AskLogonParametersViaUI {
			get {
				if(WinChangeDatabaseHelper.SkipLogonDialog) {
					return false;
				}
				return true;
			}
		}

		public override object Authenticate(DevExpress.ExpressApp.IObjectSpace objectSpace) {
			WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = false;
			try {
				return base.Authenticate(objectSpace);
			} catch {
				WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = true;
				WinChangeDatabaseHelper.SkipLogonDialog = false;
				throw;
			}
		}
	}
}
