Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security

Namespace RuntimeDbChooser.Web
    ' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    Partial Public Class RuntimeDbChooserAspNetApplication
        Inherits WebApplication

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
        End Sub
        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProviders.Add(New SecuredObjectSpaceProvider(CType(Security, SecurityStrategyComplex), args.ConnectionString, args.Connection))
            args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
        End Sub
        Private Sub CreateXPObjectSpaceProvider(ByVal connectionString As String, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
            'System.Web.HttpApplicationState application = (System.Web.HttpContext.Current != null) ? System.Web.HttpContext.Current.Application : null;
            'IXpoDataStoreProvider dataStoreProvider = null;
            'if(application != null && application["DataStoreProvider"] != null) {
            '    dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            '    e.ObjectSpaceProvider = new XPObjectSpaceProvider(dataStoreProvider, true);
            '}
            'else {
            '    if(!String.IsNullOrEmpty(connectionString)) {
            '        connectionString = DevExpress.Xpo.XpoDefault.GetConnectionPoolString(connectionString);
            '        dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString, true);
            '    }
            '    else if(e.Connection != null) {
            '        dataStoreProvider = new ConnectionDataStoreProvider(e.Connection);
            '    }
            '    if (application != null) {
            '        application["DataStoreProvider"] = dataStoreProvider;
            '    }
            '    e.ObjectSpaceProvider = new XPObjectSpaceProvider(dataStoreProvider, true);
            '}
        End Sub
        Private Sub RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
#If EASYTEST Then
            e.Updater.Update()
            e.Handled = True
#Else
            If System.Diagnostics.Debugger.IsAttached Then
                e.Updater.Update()
                e.Handled = True
            Else
                Dim message As String = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & ControlChars.CrLf & "This error occurred  because the automatic database update was disabled when the application was started without debugging." & ControlChars.CrLf & "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database using the 'DBUpdater' tool." & ControlChars.CrLf & "Anyway, refer to the following help topics for more detailed information:" & ControlChars.CrLf & "'Update Application and Database Versions' at http://help.devexpress.com/#Xaf/CustomDocument2795" & ControlChars.CrLf & "'Database Security References' at http://help.devexpress.com/#Xaf/CustomDocument3237" & ControlChars.CrLf & "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/"

                If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                    message &= ControlChars.CrLf & ControlChars.CrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
                End If
                Throw New InvalidOperationException(message)
            End If
#End If
        End Sub
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            Me.module3 = New RuntimeDbChooser.[Module].RuntimeDbChooserModule()
            Me.module4 = New RuntimeDbChooser.[Module].Web.RuntimeDbChooserAspNetModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
            Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.validationAspNetModule = New DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit
            '
            'securityStrategyComplex1
            '
            Me.securityStrategyComplex1.AllowAnonymousAccess = False
            Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
            Me.securityStrategyComplex1.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            Me.securityStrategyComplex1.UserType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser)
            '
            'authenticationStandard1
            '
            Me.authenticationStandard1.LogonParametersType = GetType(RuntimeDbChooser.[Module].BusinessObjects.CustomLogonParametersForStandardAuthentication)
            '
            'validationModule
            '
            Me.validationModule.AllowValidationDetailsAccess = True
            Me.validationModule.IgnoreWarningAndInformationRules = False
            '
            'RuntimeDbChooserAspNetApplication
            '
            Me.ApplicationName = "RuntimeDbChooser"
            Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
            Me.LinkNewObjectToParentImmediately = False
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.validationModule)
            Me.Modules.Add(Me.securityModule1)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.validationAspNetModule)
            Me.Modules.Add(Me.module4)
            Me.Security = Me.securityStrategyComplex1
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit

        End Sub
    End Class
End Namespace
