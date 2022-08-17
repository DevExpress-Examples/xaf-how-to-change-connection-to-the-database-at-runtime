Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Security

Namespace RuntimeDbChooser.Module.BusinessObjects

    Public Interface IDatabaseNameParameter

        Property DatabaseName As String

    End Interface

    <DomainComponent>
    Public Class CustomLogonParametersForStandardAuthentication
        Inherits AuthenticationStandardLogonParameters
        Implements IDatabaseNameParameter

        Private databaseNameField As String = MSSqlServerChangeDatabaseHelper.DefaultDatabaseName

        <ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)>
        Public Property DatabaseName As String Implements IDatabaseNameParameter.DatabaseName
            Get
                Return databaseNameField
            End Get

            Set(ByVal value As String)
                databaseNameField = value
            End Set
        End Property
    End Class

    <DomainComponent>
    Public Class CustomLogonParametersForActiveDirectoryAuthentication
        Implements IDatabaseNameParameter

        Private databaseNameField As String = MSSqlServerChangeDatabaseHelper.DefaultDatabaseName

        <ModelDefault("PredefinedValues", MSSqlServerChangeDatabaseHelper.Databases)>
        Public Property DatabaseName As String Implements IDatabaseNameParameter.DatabaseName
            Get
                Return databaseNameField
            End Get

            Set(ByVal value As String)
                databaseNameField = value
            End Set
        End Property
    End Class
End Namespace
