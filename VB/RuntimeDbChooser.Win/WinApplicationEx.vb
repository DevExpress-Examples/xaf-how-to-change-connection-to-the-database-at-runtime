Imports System
Imports System.Configuration
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Security
Imports RuntimeDbChooser.Module.Win
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Win
    Partial Public Class RuntimeDbChooserWindowsFormsApplication
        Inherits WinApplication
        Implements IApplicationFactory

        Protected Overrides Sub ReadLastLogonParametersCore(ByVal storage As DevExpress.ExpressApp.Utils.SettingsStorage, ByVal logonObject As Object)
            Dim standardLogonParameters As AuthenticationStandardLogonParameters = TryCast(logonObject, AuthenticationStandardLogonParameters)
            If (standardLogonParameters IsNot Nothing) AndAlso String.IsNullOrEmpty(standardLogonParameters.UserName) Then
                MyBase.ReadLastLogonParametersCore(storage, logonObject)
            End If
        End Sub
        Protected Overrides Sub OnLoggingOn(ByVal args As LogonEventArgs)
            MyBase.OnLoggingOn(args)
            MSSqlServerChangeDatabaseHelper.UpdateDatabaseName(Me, DirectCast(args.LogonParameters, IDatabaseNameParameter).DatabaseName)
        End Sub
        Protected Overrides Function OnLogonFailed(ByVal logonParameters As Object, ByVal e As Exception) As Boolean
            If WinChangeDatabaseHelper.SkipLogonDialog Then
                Return True
            End If
            Return MyBase.OnLogonFailed(logonParameters, e)
        End Function
        Private Function IApplicationFactory_CreateApplication() As WinApplication Implements IApplicationFactory.CreateApplication
            Return CreateApplication()
        End Function
        Public Shared Function CreateApplication() As RuntimeDbChooserWindowsFormsApplication
            Dim winApplication As New RuntimeDbChooserWindowsFormsApplication()

            CType(winApplication.Security, SecurityStrategyComplex).Authentication = New WinChangeDatabaseStandardAuthentication()

            'WinChangeDatabaseActiveDirectoryAuthentication activeDirectoryAuthentication = new WinChangeDatabaseActiveDirectoryAuthentication();
            'activeDirectoryAuthentication.CreateUserAutomatically = true;
            '((SecurityStrategyComplex)winApplication.Security).Authentication = activeDirectoryAuthentication;

            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If
            Return winApplication
        End Function
    End Class
End Namespace
