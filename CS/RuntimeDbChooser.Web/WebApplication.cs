using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;

namespace RuntimeDbChooser.Web {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class RuntimeDbChooserAspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private RuntimeDbChooser.Module.RuntimeDbChooserModule module3;
        private RuntimeDbChooser.Module.Web.RuntimeDbChooserAspNetModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule;

        public RuntimeDbChooserAspNetApplication() {
            InitializeComponent();
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(args.ConnectionString, null, true));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void CreateXPObjectSpaceProvider(string connectionString, CreateCustomObjectSpaceProviderEventArgs e) {
            //System.Web.HttpApplicationState application = (System.Web.HttpContext.Current != null) ? System.Web.HttpContext.Current.Application : null;
            //IXpoDataStoreProvider dataStoreProvider = null;
            //if(application != null && application["DataStoreProvider"] != null) {
            //    dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            //    e.ObjectSpaceProvider = new XPObjectSpaceProvider(dataStoreProvider, true);
            //}
            //else {
            //    if(!String.IsNullOrEmpty(connectionString)) {
            //        connectionString = DevExpress.Xpo.XpoDefault.GetConnectionPoolString(connectionString);
            //        dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString, true);
            //    }
            //    else if(e.Connection != null) {
            //        dataStoreProvider = new ConnectionDataStoreProvider(e.Connection);
            //    }
            //    if (application != null) {
            //        application["DataStoreProvider"] = dataStoreProvider;
            //    }
            //    e.ObjectSpaceProvider = new XPObjectSpaceProvider(dataStoreProvider, true);
            //}
        }
        private void RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                string message = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the following help topics for more detailed information:\r\n" +
                    "'Update Application and Database Versions' at http://help.devexpress.com/#Xaf/CustomDocument2795\r\n" +
                    "'Database Security References' at http://help.devexpress.com/#Xaf/CustomDocument3237\r\n" +
                    "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new RuntimeDbChooser.Module.RuntimeDbChooserModule();
            this.module4 = new RuntimeDbChooser.Module.Web.RuntimeDbChooserAspNetModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            this.securityStrategyComplex1.UserType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(RuntimeDbChooser.Module.BusinessObjects.CustomLogonParametersForStandardAuthentication);
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // RuntimeDbChooserAspNetApplication
            // 
            this.ApplicationName = "RuntimeDbChooser";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.LinkNewObjectToParentImmediately = false;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.module4);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
