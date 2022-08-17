Imports DevExpress.Xpo.DB.Helpers

Namespace RuntimeDbChooser.Module.BusinessObjects

    Public Class MSSqlServerChangeDatabaseHelper

        Public Const Databases As String = "E1344_DB1;E1344_DB2"

        Private Shared ReadOnly _defaultDatabaseName As String = Databases.Split(";"c)(0)

        Public Shared ReadOnly Property DefaultDatabaseName As String
            Get
                Return _defaultDatabaseName
            End Get
        End Property

        Public Shared Function PatchConnectionString(ByVal databaseName As String, ByVal connectionString As String) As String
            Dim helper As ConnectionStringParser = New ConnectionStringParser(connectionString)
            helper.RemovePartByName("Initial Catalog")
            Return String.Format("Initial Catalog={0};{1}", databaseName, helper.GetConnectionString())
        End Function
    End Class
End Namespace
