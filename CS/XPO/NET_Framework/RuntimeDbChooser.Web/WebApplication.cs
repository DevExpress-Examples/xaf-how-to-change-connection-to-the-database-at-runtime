using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RuntimeDbChooser.Web {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
    public partial class RuntimeDbChooserAspNetApplication : WebApplication {
        private static ConcurrentDictionary<string, bool> isCompatibilityChecked = new ConcurrentDictionary<string, bool>();
        private static Dictionary<string, IXpoDataStoreProvider> xpoDataStoreProviderDictionary = new Dictionary<string, IXpoDataStoreProvider>();

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
            //ActiveDirectoryAuthentication
            //((SecurityStrategyComplex)Security).Authentication = new Module.ChangeDatabaseActiveDirectoryAuthentication();
            //((SecurityStrategyComplex)Security).Authentication.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            //((SecurityStrategyComplex)Security).Authentication.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
        }

        protected override LogonController CreateLogonController() {
            return CreateController<Controllers.CustomLogonController>();
        }
        protected override bool IsCompatibilityChecked {
            get {
                return isCompatibilityChecked.ContainsKey(ConnectionString);
            }

            set {
                isCompatibilityChecked.TryAdd(ConnectionString, value);
            }
        }
        protected override IViewUrlManager CreateViewUrlManager() {
            return new ViewUrlManager();
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            IXpoDataStoreProvider xpoDataStoreProvider;
            lock(xpoDataStoreProviderDictionary) {
                if(!xpoDataStoreProviderDictionary.TryGetValue(connectionString, out xpoDataStoreProvider)) {
                    //TODO Minakov the enablePoolingInConnectionString parameter should be true. Change after IObjectSpace.DataBase fix
                    xpoDataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, false);
                    xpoDataStoreProviderDictionary[connectionString] = xpoDataStoreProvider;
                }
            }
            return xpoDataStoreProvider;
        }
        private void RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            } else {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

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
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
            this.securityStrategyComplex1.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            // 
            // securityModule1
            // 
            this.securityModule1.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(Module.BusinessObjects.CustomLogonParametersForStandardAuthentication);
            this.authenticationStandard1.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            this.authenticationStandard1.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
            // 
            // RuntimeDbChooserAspNetApplication
            // 
            this.ApplicationName = "RuntimeDbChooser";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Security = this.securityStrategyComplex1;
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.validationAspNetModule);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.RuntimeDbChooserAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
