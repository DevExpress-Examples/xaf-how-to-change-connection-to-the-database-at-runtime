Imports System.Web
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Module.Web
    Public Class WebChangeDatabaseAuthenticationActiveDirectory
        Inherits AuthenticationActiveDirectory(Of SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication)

        Public Sub New()
            Me.CreateUserAutomatically = True
        End Sub
        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
            Get
                Dim databaseName As String = HttpContext.Current.Request.Params(WebChangeDatabaseController.DatabaseParameterName)
                If String.IsNullOrEmpty(databaseName) Then
                    Return True
                End If
                Return MyBase.AskLogonParametersViaUI
            End Get
        End Property
    End Class
End Namespace
