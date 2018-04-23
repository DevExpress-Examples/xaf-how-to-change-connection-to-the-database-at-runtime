using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Module.Win {
	public interface IApplicationFactory {
		WinApplication CreateApplication();
	}

	public class WinChangeDatabaseController : WindowController {
		public SingleChoiceAction changeDatabaseAction;
		public WinChangeDatabaseController() {
			this.TargetWindowType = WindowType.Main;
			changeDatabaseAction = new SingleChoiceAction(this, "ChangeDatabase", PredefinedCategory.View);
			foreach(string databaseName in MSSqlServerChangeDatabaseHelper.Databases.Split(';')) {
				changeDatabaseAction.Items.Add(new ChoiceActionItem(databaseName, databaseName));
			}
			changeDatabaseAction.Execute += new SingleChoiceActionExecuteEventHandler(changeDatabaseAction_Execute);
		}


		protected override void OnActivated() {
			base.OnActivated();
			changeDatabaseAction.Active["ActiveOnlyForAdmin"] = SecuritySystem.CurrentUserName == "Admin";
			foreach(ChoiceActionItem item in changeDatabaseAction.Items) {
				if(Application.ConnectionString.Contains((string)item.Data)) {
					changeDatabaseAction.SelectedItem = item;
					break;
				}
			}
			Application.LoggedOn += new EventHandler<LogonEventArgs>(Application_LoggedOn);
			Application.LoggedOff += new EventHandler<EventArgs>(Application_LoggedOff);
		}

		void Application_LoggedOff(object sender, EventArgs e) {
			if(!string.IsNullOrEmpty(WinChangeDatabaseHelper.DatabaseName)) {
				((IDatabaseNameParameter)SecuritySystem.LogonParameters).DatabaseName = WinChangeDatabaseHelper.DatabaseName;
			}
			AuthenticationStandardLogonParameters authenticationStandardLogonParameters = SecuritySystem.LogonParameters as AuthenticationStandardLogonParameters;
			if(authenticationStandardLogonParameters != null && !string.IsNullOrEmpty(WinChangeDatabaseStandardAuthentication.AuthenticatedUserName)) {
				authenticationStandardLogonParameters.UserName = WinChangeDatabaseStandardAuthentication.AuthenticatedUserName;
			}
		}

		void Application_LoggedOn(object sender, LogonEventArgs e) {
			WinChangeDatabaseHelper.SkipLogonDialog = false;
		}
		void changeDatabaseAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {

			WinChangeDatabaseHelper.DatabaseName = (string)e.SelectedChoiceActionItem.Data;
			WinChangeDatabaseHelper.SkipLogonDialog = true;
			WinChangeDatabaseStandardAuthentication.AuthenticatedUserName = SecuritySystem.CurrentUserName;

			Frame.GetController<LogoffController>().LogoffAction.DoExecute();
		}
	}
}
