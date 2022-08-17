Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Win.Controllers

    Public Class CustomLogonController
        Inherits LogonController

        Protected Overrides Sub Accept(ByVal args As SimpleActionExecuteEventArgs)
            Dim targetDataBaseName As String = CType(Application.Security.LogonParameters, IDatabaseNameParameter).DatabaseName
            Dim newConnectionString As String = MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, Application.ConnectionString)
            If Not Equals(Application.ConnectionString, newConnectionString) Then
                Application.ObjectSpaceProviderContainer?.Clear()
                Application.ConnectionString = newConnectionString
            End If

            MyBase.Accept(args)
        End Sub
    End Class
End Namespace
