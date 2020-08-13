using System;
using System.Web;
using System.Web.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Actions;
using RuntimeDbChooser.Module.BusinessObjects;
using DevExpress.Persistent.Base;

namespace RuntimeDbChooser.Module.Web {
	public class WebChangeDatabaseController : WindowController {
		public const string DatabaseParameterName = "DatabaseName";
		public SingleChoiceAction changeDatabaseAction;
		public WebChangeDatabaseController() {
			TargetWindowType = WindowType.Main;

			changeDatabaseAction = new SingleChoiceAction(this, "ChangeDatabase", PredefinedCategory.View);
			foreach(string databaseName in MSSqlServerChangeDatabaseHelper.Databases.Split(';')) {
				changeDatabaseAction.Items.Add(new ChoiceActionItem(databaseName, databaseName));
			}
			changeDatabaseAction.Execute += new SingleChoiceActionExecuteEventHandler(changeDatabaseAction_Execute);
		}

		protected override void OnActivated() {
			base.OnActivated();
			foreach(ChoiceActionItem item in changeDatabaseAction.Items) {
				if(Application.ConnectionString.Contains((string)item.Data)) {
					changeDatabaseAction.SelectedItem = item;
					break;
				}
			}
		}
		void changeDatabaseAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
			SecuritySystem.Instance.Logoff();
			HttpContext.Current.Session.Abandon();
			WebApplication.Redirect(string.Format("{0}?{1}={2}", FormsAuthentication.DefaultUrl, DatabaseParameterName, (string)e.SelectedChoiceActionItem.Data));
		}
	}
}
