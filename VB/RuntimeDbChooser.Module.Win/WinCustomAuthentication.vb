Imports System
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports RuntimeDbChooser.Module.BusinessObjects
Imports DevExpress.ExpressApp.Security.Strategy

Namespace RuntimeDbChooser.Module.Win
    Public Class WinChangeDatabaseHelper

        Private Shared skipLogonDialog_Renamed As Boolean = False
        Public Shared DatabaseName As String
        Public Shared AuthenticatedUserLogonFailed As Boolean = False
        Public Shared Property SkipLogonDialog() As Boolean
            Get
                Return skipLogonDialog_Renamed
            End Get
            Set(ByVal value As Boolean)
                skipLogonDialog_Renamed = value
            End Set
        End Property
    End Class

    Public Class WinChangeDatabaseStandardAuthentication
        Inherits AuthenticationStandard(Of SecuritySystemUser, CustomLogonParametersForStandardAuthentication)

        Public Shared AuthenticatedUserName As String

        Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
            Get
                If WinChangeDatabaseHelper.SkipLogonDialog Then
                    Return False
                End If
                Return MyBase.AskLogonParametersViaUI
            End Get
        End Property
        Public Overrides Function Authenticate(ByVal objectSpace As DevExpress.ExpressApp.IObjectSpace) As Object
            WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = False
            If String.IsNullOrEmpty(AuthenticatedUserName) Then
                Return MyBase.Authenticate(objectSpace)
            Else
                Dim logonParameters As CustomLogonParametersForStandardAuthentication = CType(LogonParameters, CustomLogonParametersForStandardAuthentication)
                Dim result As Object = objectSpace.FindObject(UserType, New BinaryOperator("UserName", logonParameters.UserName))
                If result Is Nothing Then
                    WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = True
                    WinChangeDatabaseHelper.SkipLogonDialog = False
                    Throw New AuthenticationException(logonParameters.UserName, SecurityExceptionLocalizer.GetExceptionMessage(SecurityExceptionId.RetypeTheInformation))
                End If
                AuthenticatedUserName = ""
                Return result
            End If
        End Function
    End Class

    Public Class WinChangeDatabaseActiveDirectoryAuthentication
        Inherits AuthenticationActiveDirectory(Of SecuritySystemUser, CustomLogonParametersForActiveDirectoryAuthentication)

        Public Overrides ReadOnly Property IsLogoffEnabled() As Boolean
            Get
                Return True
            End Get
        End Property
        Public Overrides ReadOnly Property AskLogonParametersViaUI() As Boolean
            Get
                If WinChangeDatabaseHelper.SkipLogonDialog Then
                    Return False
                End If
                Return True
            End Get
        End Property

        Public Overrides Function Authenticate(ByVal objectSpace As DevExpress.ExpressApp.IObjectSpace) As Object
            WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = False
            Try
                Return MyBase.Authenticate(objectSpace)
            Catch
                WinChangeDatabaseHelper.AuthenticatedUserLogonFailed = True
                WinChangeDatabaseHelper.SkipLogonDialog = False
                Throw
            End Try
        End Function
    End Class
End Namespace
