#If Not NET
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Module

    Public Class ChangeDatabaseActiveDirectoryAuthentication
        Inherits AuthenticationActiveDirectory(Of SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication)

        Public Sub New()
            CreateUserAutomatically = True
        End Sub

        Public Overrides ReadOnly Property IsLogoffEnabled As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property AskLogonParametersViaUI As Boolean
            Get
                Return True
            End Get
        End Property
    End Class
End Namespace
#End If
