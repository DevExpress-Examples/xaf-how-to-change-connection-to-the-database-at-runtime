using DevExpress.Xpo.DB.Helpers;

namespace RuntimeDbChooser.Module.BusinessObjects;
public class MSSqlServerChangeDatabaseHelper {
    public const string Databases = "E1344_EFCore_DB1;E1344_EFCore_DB2";
    static readonly string _defaultDatabaseName = MSSqlServerChangeDatabaseHelper.Databases.Split(';')[0];

    public static string DefaultDatabaseName => _defaultDatabaseName;

    public static string PatchConnectionString(string databaseName, string connectionString) {
        ConnectionStringParser helper = new ConnectionStringParser(connectionString);
        helper.RemovePartByName("Initial Catalog");
        return string.Format("Initial Catalog={0};{1}", databaseName, helper.GetConnectionString());
    }
}
