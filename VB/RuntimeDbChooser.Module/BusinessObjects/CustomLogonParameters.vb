Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Security

Namespace RuntimeDbChooser.Module.BusinessObjects
    Public Interface IDatabaseNameParameter
        Property DatabaseName() As String
    End Interface
    <DomainComponent> _
    Public Class CustomLogonParametersForStandardAuthentication
        Inherits AuthenticationStandardLogonParameters
        Implements IDatabaseNameParameter


        Private databaseName_Renamed As String = MSSqlServerChangeDatabaseHelper.Databases.Split(";"c)(0)
        <ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)> _
        Public Property DatabaseName() As String Implements IDatabaseNameParameter.DatabaseName
            Get
                Return databaseName_Renamed
            End Get
            Set(ByVal value As String)
                databaseName_Renamed = value
            End Set
        End Property
    End Class
    <DomainComponent> _
    Public Class CustomLogonParametersForActiveDirectoryAuthentication
        Implements IDatabaseNameParameter


        Private databaseName_Renamed As String = MSSqlServerChangeDatabaseHelper.Databases.Split(";"c)(0)

        <ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)> _
        Public Property DatabaseName() As String Implements IDatabaseNameParameter.DatabaseName
            Get
                Return databaseName_Renamed
            End Get
            Set(ByVal value As String)
                databaseName_Renamed = value
            End Set
        End Property
    End Class
    Public Class MSSqlServerChangeDatabaseHelper
        Public Const Databases As String = "E1344_DB1;E1344_DB2"
        Public Shared Sub UpdateDatabaseName(ByVal application As XafApplication, ByVal databaseName As String)
            Dim helper As New ConnectionStringParser(application.ConnectionString)
            helper.RemovePartByName("Initial Catalog")
            application.ConnectionString = String.Format("Initial Catalog={0};{1}", databaseName, helper.GetConnectionString())
        End Sub
    End Class
End Namespace
