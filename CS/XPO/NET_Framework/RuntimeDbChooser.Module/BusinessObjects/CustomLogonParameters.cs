using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;

namespace RuntimeDbChooser.Module.BusinessObjects {
    public interface IDatabaseNameParameter {
        string DatabaseName { get; set; }
    }
    [DomainComponent]
    public class CustomLogonParametersForStandardAuthentication : AuthenticationStandardLogonParameters, IDatabaseNameParameter {
        private string databaseName = MSSqlServerChangeDatabaseHelper.DefaultDatabaseName;
        [ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)]
        public string DatabaseName {
            get { return databaseName; }
            set { databaseName = value; }
        }
    }
    [DomainComponent]
    public class CustomLogonParametersForActiveDirectoryAuthentication : IDatabaseNameParameter {
        private string databaseName = MSSqlServerChangeDatabaseHelper.DefaultDatabaseName;

        [ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)]
        public string DatabaseName {
            get { return databaseName; }
            set { databaseName = value; }
        }
    }
}
