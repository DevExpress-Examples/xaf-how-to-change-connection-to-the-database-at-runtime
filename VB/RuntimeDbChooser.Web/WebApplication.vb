Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports System.Collections.Concurrent
Imports RuntimeDbChooser.Module.BusinessObjects

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
Partial Public Class RuntimeDbChooserAspNetApplication
    Inherits WebApplication
    Private Shared _isCompatibilityChecked As ConcurrentDictionary(Of String, Boolean) = New ConcurrentDictionary(Of String, Boolean)()
    Private Shared xpoDataStoreProviderDictionary As Dictionary(Of String, IXpoDataStoreProvider) = New Dictionary(Of String, IXpoDataStoreProvider)()

    Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
    Private module3 As RuntimeDbChooser.Module.RuntimeDbChooserModule
    Private module4 As RuntimeDbChooser.Module.Web.RuntimeDbChooserAspNetModule

    Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
    Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
    Private validationAspNetModule As DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule

    Public Sub New()
        InitializeComponent()
        ''CType(Security, SecurityStrategyComplex).Authentication = New RuntimeDbChooser.Module.ChangeDatabaseActiveDirectoryAuthentication()
    End Sub


    Protected Overrides Sub OnLoggingOn(ByVal args As LogonEventArgs)
        MyBase.OnLoggingOn(args)
        Dim targetDataBaseName As String = CType(args.LogonParameters, IDatabaseNameParameter).DatabaseName
        CType(ObjectSpaceProviders(0), XPObjectSpaceProvider).SetDataStoreProvider(GetDataStoreProvider(MSSqlServerChangeDatabaseHelper.PatchConnectionString(targetDataBaseName, ConnectionString), Nothing))
    End Sub
    Protected Overrides Property IsCompatibilityChecked As Boolean
        Get
            Return _isCompatibilityChecked.GetOrAdd(ConnectionString, False)
        End Get
        Set(ByVal value As Boolean)
            _isCompatibilityChecked.TryAdd(ConnectionString, value)
        End Set
    End Property
    Protected Overrides Function CreateViewUrlManager() As IViewUrlManager
        Return New ViewUrlManager()
    End Function
    Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
        args.ObjectSpaceProvider = New SecuredObjectSpaceProvider(DirectCast(Me.Security, ISelectDataSecurityProvider), GetDataStoreProvider(args.ConnectionString, args.Connection), True)
        args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
    End Sub
    Private Function GetDataStoreProvider(ByVal connectionString As String, ByVal connection As IDbConnection) As IXpoDataStoreProvider
        Dim xpoDataStoreProvider As IXpoDataStoreProvider = Nothing

        SyncLock xpoDataStoreProviderDictionary

            If Not xpoDataStoreProviderDictionary.TryGetValue(connectionString, xpoDataStoreProvider) Then
                xpoDataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, False)
                xpoDataStoreProviderDictionary(connectionString) = xpoDataStoreProvider
            End If
        End SyncLock

        Return xpoDataStoreProvider
    End Function
    Private Sub RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
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
    Private Sub InitializeComponent()
        Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
        Me.module3 = New RuntimeDbChooser.Module.RuntimeDbChooserModule()
        Me.module4 = New RuntimeDbChooser.Module.Web.RuntimeDbChooserAspNetModule()
        Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
        Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
        Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
        Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
        Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
        Me.validationAspNetModule = New DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        ' 
        ' securityStrategyComplex1
        ' 
        Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
        Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
        Me.securityStrategyComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
        ' 
        ' securityModule1
        ' 
        Me.securityModule1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
        ' 
        ' authenticationStandard1
        ' 
        Me.authenticationStandard1.LogonParametersType = GetType(RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication)
        ' 
        ' RuntimeDbChooserAspNetApplication
        ' 
        Me.ApplicationName = "RuntimeDbChooser"
        Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
        Me.Modules.Add(Me.module1)
        Me.Modules.Add(Me.module2)
        Me.Modules.Add(Me.module3)
        Me.Modules.Add(Me.module4)
        Me.Modules.Add(Me.securityModule1)
        Me.Security = Me.securityStrategyComplex1
        Me.Modules.Add(Me.validationModule)
        Me.Modules.Add(Me.validationAspNetModule)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
End Class

