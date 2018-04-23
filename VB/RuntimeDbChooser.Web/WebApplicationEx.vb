Imports System
Imports System.Web
Imports System.Web.Security
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports RuntimeDbChooser.Module.Web
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Web
    Partial Public Class RuntimeDbChooserAspNetApplication
        Inherits WebApplication

        Protected Overrides Sub ReadSecuredLogonParameters()
            MyBase.ReadSecuredLogonParameters() ' the "UserName" is restored in the base method.

            Dim databaseName As String = HttpContext.Current.Request.Params(WebChangeDatabaseController.DatabaseParameterName)
            If Not String.IsNullOrEmpty(databaseName) Then
                DirectCast(SecuritySystem.LogonParameters, IDatabaseNameParameter).DatabaseName = databaseName
            End If
        End Sub

        Private canReadSecuredLogonParameters_Renamed As Boolean = True
        Protected Overrides Function CanReadSecuredLogonParameters() As Boolean
            If Not canReadSecuredLogonParameters_Renamed Then
                Return False
            End If
            Return MyBase.CanReadSecuredLogonParameters()
        End Function
        Protected Overrides Function OnLogonFailed(ByVal logonParameters As Object, ByVal e As Exception) As Boolean
            If CanReadSecuredLogonParameters() Then
                FormsAuthentication.SignOut()
                canReadSecuredLogonParameters_Renamed = False
                Try
                    Start()
                Finally
                    canReadSecuredLogonParameters_Renamed = True
                End Try
                Return True
            Else
                Return MyBase.OnLogonFailed(logonParameters, e)
            End If
        End Function
        Protected Overrides Sub ReadLastLogonParametersCore(ByVal storage As DevExpress.ExpressApp.Utils.SettingsStorage, ByVal logonObject As Object)
            'string databaseName = HttpContext.Current.Request.Params[WebChangeDatabaseController.DatabaseParameterName];
            If String.IsNullOrEmpty(DirectCast(logonObject, IDatabaseNameParameter).DatabaseName) Then
                MyBase.ReadLastLogonParametersCore(storage, logonObject)
            End If
        End Sub
        Protected Overrides Sub OnLoggingOn(ByVal args As LogonEventArgs)
            MyBase.OnLoggingOn(args)
            MSSqlServerChangeDatabaseHelper.UpdateDatabaseName(Me, DirectCast(args.LogonParameters, IDatabaseNameParameter).DatabaseName)
        End Sub
    End Class
End Namespace