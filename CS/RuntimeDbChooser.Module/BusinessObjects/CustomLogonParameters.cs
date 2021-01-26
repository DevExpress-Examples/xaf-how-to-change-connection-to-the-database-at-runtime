using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo.DB.Helpers;

namespace RuntimeDbChooser.Module.BusinessObjects {
    public interface IDatabaseNameParameter {
		string DatabaseName { get; set; }
	}
	[DomainComponent]
	public class CustomLogonParametersForStandardAuthentication : AuthenticationStandardLogonParameters, IDatabaseNameParameter {
		private string databaseName = MSSqlServerChangeDatabaseHelper.Databases.Split(';')[0];
		[ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)]
		public string DatabaseName {
			get { return databaseName; }
			set { databaseName = value; }
		}
	}
	[DomainComponent]
	public class CustomLogonParametersForActiveDirectoryAuthentication : IDatabaseNameParameter {
		private string databaseName = MSSqlServerChangeDatabaseHelper.Databases.Split(';')[0];

		[ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)]
		public string DatabaseName {
			get { return databaseName; }
			set { databaseName = value; }
		}
	}

    public class MSSqlServerChangeDatabaseHelper {
		public const string Databases = "E1344_DB1;E1344_DB2";
        public static string PatchConnectionString(string databaseName, string connectionString) {
            ConnectionStringParser helper = new ConnectionStringParser(connectionString);
            helper.RemovePartByName("Initial Catalog");
            return string.Format("Initial Catalog={0};{1}", databaseName, helper.GetConnectionString());
        }
    }
}
