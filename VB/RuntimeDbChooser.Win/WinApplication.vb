Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security.Strategy
Imports RuntimeDbChooser.Module.BusinessObjects

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
Partial Public Class RuntimeDbChooserWindowsFormsApplication
    Inherits WinApplication

    Private Shared _isCompatibilityChecked As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)()

    Public Sub New()
        InitializeComponent()
        CType(Security, SecurityStrategyComplex).Authentication = New AuthenticationStandard(Of SecuritySystemUser, CustomLogonParametersForStandardAuthentication)()
        ''CType(Security, SecurityStrategyComplex).Authentication = New RuntimeDbChooser.Module.ChangeDatabaseActiveDirectoryAuthentication()
        SplashScreen = New DXSplashScreen(GetType(XafSplashScreen), New DefaultOverlayFormOptions())
    End Sub

    Protected Overrides Sub OnLoggingOn(ByVal args As LogonEventArgs)
        MyBase.OnLoggingOn(args)
        Dim targetDataBaseName As String = CType(args.LogonParameters, IDatabaseNameParameter).DatabaseName
        ObjectSpaceProvider.ConnectionString = MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, ConnectionString)
    End Sub
    Protected Overrides Property IsCompatibilityChecked As Boolean
        Get
            Return If(_isCompatibilityChecked.ContainsKey(ConnectionString), _isCompatibilityChecked(ConnectionString), False)
        End Get
        Set(ByVal value As Boolean)
            _isCompatibilityChecked(ConnectionString) = value
        End Set
    End Property
    Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
        args.ObjectSpaceProviders.Add(New SecuredObjectSpaceProvider(DirectCast(Security, SecurityStrategyComplex), XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, False), False))
        args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
    End Sub
    Private Sub RuntimeDbChooserWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
        e.Updater.Update()
        e.Handled = True
#Else
        If System.Diagnostics.Debugger.IsAttached Then
            e.Updater.Update()
            e.Handled = True
        Else
            Dim message As String = "The application cannot connect to the specified database, " &
                "because the database doesn't exist, its version is older " &
                "than that of the application or its schema does not match " &
                "the ORM data model structure. To avoid this error, use one " &
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."

            If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                message &= Constants.vbCrLf & Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
            End If
            Throw New InvalidOperationException(message)
        End If
#End If
    End Sub
    Private Shared Sub RuntimeDbChooserWindowsFormsApplication_CustomizeLanguagesList(ByVal sender As Object, ByVal e As CustomizeLanguagesListEventArgs) Handles MyBase.CustomizeLanguagesList
        Dim userLanguageName As String = System.Threading.Thread.CurrentThread.CurrentUICulture.Name
        If userLanguageName <> "en-US" And e.Languages.IndexOf(userLanguageName) = -1 Then
            e.Languages.Add(userLanguageName)
        End If
    End Sub
End Class
