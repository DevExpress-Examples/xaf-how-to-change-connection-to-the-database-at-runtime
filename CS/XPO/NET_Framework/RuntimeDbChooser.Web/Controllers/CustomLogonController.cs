using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web;
using RuntimeDbChooser.Module.BusinessObjects;

namespace RuntimeDbChooser.Web.Controllers {
    public class CustomLogonController : WebLogonController {
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
