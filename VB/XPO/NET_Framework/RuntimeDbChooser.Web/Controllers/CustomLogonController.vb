Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Web
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Web.Controllers

    Public Class CustomLogonController
        Inherits WebLogonController

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
