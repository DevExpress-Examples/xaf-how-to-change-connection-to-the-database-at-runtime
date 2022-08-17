Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo
Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic

Namespace RuntimeDbChooser.Web

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
    Public Partial Class RuntimeDbChooserAspNetApplication
        Inherits WebApplication

        Private Shared isCompatibilityCheckedField As ConcurrentDictionary(Of String, Boolean) = New ConcurrentDictionary(Of String, Boolean)()

        Private Shared xpoDataStoreProviderDictionary As Dictionary(Of String, IXpoDataStoreProvider) = New Dictionary(Of String, IXpoDataStoreProvider)()

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule

        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule

        Private module3 As [Module].RuntimeDbChooserModule

        Private module4 As [Module].Web.RuntimeDbChooserAspNetModule

        Private securityModule1 As SecurityModule

        Private securityStrategyComplex1 As SecurityStrategyComplex

        Private authenticationStandard1 As AuthenticationStandard

        Private validationModule As Validation.ValidationModule

        Private validationAspNetModule As Validation.Web.ValidationAspNetModule

        Public Sub New()
            InitializeComponent()
        'ActiveDirectoryAuthentication
        '((SecurityStrategyComplex)Security).Authentication = new Module.ChangeDatabaseActiveDirectoryAuthentication();
        '((SecurityStrategyComplex)Security).Authentication.UserType = typeof(Module.BusinessObjects.ApplicationUser);
        '((SecurityStrategyComplex)Security).Authentication.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
        End Sub

        Protected Overrides Function CreateLogonController() As LogonController
            Return CreateController(Of Controllers.CustomLogonController)()
        End Function

        Protected Overrides Property IsCompatibilityChecked As Boolean
            Get
                Return isCompatibilityCheckedField.GetOrAdd(ConnectionString, False)
            End Get

            Set(ByVal value As Boolean)
                isCompatibilityCheckedField.TryAdd(ConnectionString, value)
            End Set
        End Property

        Protected Overrides Function CreateViewUrlManager() As IViewUrlManager
            Return New ViewUrlManager()
        End Function

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(Security, SecurityStrategyComplex), GetDataStoreProvider(args.ConnectionString, args.Connection), True)
            args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
        End Sub

        Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As System.Data.IDbConnection) As IXpoDataStoreProvider
            Dim xpoDataStoreProvider As IXpoDataStoreProvider
            SyncLock xpoDataStoreProviderDictionary
                If Not xpoDataStoreProviderDictionary.TryGetValue(connectionString, xpoDataStoreProvider) Then
                    'TODO Minakov the enablePoolingInConnectionString parameter should be true. Change after IObjectSpace.DataBase fix
                    xpoDataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, False)
                    xpoDataStoreProviderDictionary(connectionString) = xpoDataStoreProvider
                End If

            End SyncLock

            Return xpoDataStoreProvider
        End Function

        Private Sub RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DatabaseVersionMismatchEventArgs)
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

        Private Sub InitializeComponent()
            module1 = New SystemModule.SystemModule()
            module2 = New SystemModule.SystemAspNetModule()
            module3 = New [Module].RuntimeDbChooserModule()
            module4 = New [Module].Web.RuntimeDbChooserAspNetModule()
            securityModule1 = New SecurityModule()
            securityStrategyComplex1 = New SecurityStrategyComplex()
            securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
            authenticationStandard1 = New AuthenticationStandard()
            validationModule = New Validation.ValidationModule()
            validationAspNetModule = New Validation.Web.ValidationAspNetModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            securityStrategyComplex1.Authentication = authenticationStandard1
            securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            securityStrategyComplex1.UserType = GetType([Module].BusinessObjects.ApplicationUser)
            ' 
            ' securityModule1
            ' 
            securityModule1.UserType = GetType([Module].BusinessObjects.ApplicationUser)
            ' 
            ' authenticationStandard1
            ' 
            authenticationStandard1.LogonParametersType = GetType([Module].BusinessObjects.CustomLogonParametersForStandardAuthentication)
            authenticationStandard1.UserType = GetType([Module].BusinessObjects.ApplicationUser)
            authenticationStandard1.UserLoginInfoType = GetType([Module].BusinessObjects.ApplicationUserLoginInfo)
            ' 
            ' RuntimeDbChooserAspNetApplication
            ' 
            ApplicationName = "RuntimeDbChooser"
            CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema
            Modules.Add(module1)
            Modules.Add(module2)
            Modules.Add(module3)
            Modules.Add(module4)
            Modules.Add(securityModule1)
            Security = securityStrategyComplex1
            Modules.Add(validationModule)
            Modules.Add(validationAspNetModule)
            AddHandler DatabaseVersionMismatch, New EventHandler(Of DatabaseVersionMismatchEventArgs)(AddressOf RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub
    End Class
End Namespace
