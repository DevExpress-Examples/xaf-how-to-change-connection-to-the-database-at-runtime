using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Win.Controllers {
    public class CustomLogonController : LogonController {
        protected override void Accept(SimpleActionExecuteEventArgs args) {
            string targetDataBaseName = ((IDatabaseNameParameter)Application.Security.LogonParameters).DatabaseName;
            string newConnectionString = MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, Application.ConnectionString);
            if(Application.ConnectionString != newConnectionString) {
                Application.ObjectSpaceProviderContainer?.Clear();
                Application.ConnectionString = newConnectionString;
            }
            base.Accept(args);
        }
    }
}
