Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.ExpressApp.Xpo
Imports System
Imports System.Collections.Generic

Namespace RuntimeDbChooser.Win

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
    Public Partial Class RuntimeDbChooserWindowsFormsApplication
        Inherits WinApplication

        Private Shared isCompatibilityCheckedField As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)()

        Public Sub New()
            InitializeComponent()
            'ActiveDirectoryAuthentication
            '((SecurityStrategyComplex)Security).Authentication = new Module.ChangeDatabaseActiveDirectoryAuthentication();
            '((SecurityStrategyComplex)Security).Authentication.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            '((SecurityStrategyComplex)Security).Authentication.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
            SplashScreen = New DXSplashScreen(GetType(XafSplashScreen), New DefaultOverlayFormOptions())
        End Sub

        Protected Overrides Function CreateLogonController() As LogonController
            Return CreateController(Of Controllers.CustomLogonController)()
        End Function

        Protected Overrides Property IsCompatibilityChecked As Boolean
            Get
                Return If(isCompatibilityCheckedField.ContainsKey(ConnectionString), isCompatibilityCheckedField(ConnectionString), False)
            End Get

            Set(ByVal value As Boolean)
                isCompatibilityCheckedField(ConnectionString) = value
            End Set
        End Property

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProviders.Add(New SecuredObjectSpaceProvider(CType(Security, SecurityStrategyComplex), XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, False), False))
            args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
        End Sub

        Private Sub RuntimeDbChooserWindowsFormsApplication_CustomizeLanguagesList(ByVal sender As Object, ByVal e As CustomizeLanguagesListEventArgs)
            Dim userLanguageName As String = Threading.Thread.CurrentThread.CurrentUICulture.Name
            If Not Equals(userLanguageName, "en-US") AndAlso e.Languages.IndexOf(userLanguageName) = -1 Then
                e.Languages.Add(userLanguageName)
            End If
        End Sub

        Private Sub RuntimeDbChooserWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DatabaseVersionMismatchEventArgs)
#If EASYTEST
            e.Updater.Update();
            e.Handled = true;
#Else
            If System.Diagnostics.Debugger.IsAttached Then
                e.Updater.Update()
                e.Handled = True
            Else
                Dim message As String = "The application cannot connect to the specified database, " & "because the database doesn't exist, its version is older " & "than that of the application or its schema does not match " & "the ORM data model structure. To avoid this error, use one " & "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."
                If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                    message += Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
                End If

                Throw New InvalidOperationException(message)
            End If
#End If
        End Sub
    End Class
End Namespace
