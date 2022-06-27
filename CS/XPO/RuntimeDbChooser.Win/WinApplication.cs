using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.ExpressApp.Xpo;
using RuntimeDbChooser.Module.BusinessObjects;
using System;
using System.Collections.Generic;

namespace RuntimeDbChooser.Win {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
    public partial class RuntimeDbChooserWindowsFormsApplication : WinApplication {
        private static Dictionary<string, bool> isCompatibilityChecked = new Dictionary<string, bool>();

        public RuntimeDbChooserWindowsFormsApplication() {
            InitializeComponent();
            //ActiveDirectoryAuthentication
            //((SecurityStrategyComplex)Security).Authentication = new Module.ChangeDatabaseActiveDirectoryAuthentication();
            //((SecurityStrategyComplex)Security).Authentication.UserType = typeof(Module.BusinessObjects.ApplicationUser);
            //((SecurityStrategyComplex)Security).Authentication.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
            SplashScreen = new DXSplashScreen(typeof(XafSplashScreen), new DefaultOverlayFormOptions());
        }
        protected override LogonController CreateLogonController() {
            return CreateController<Controllers.CustomLogonController>();
        }
        protected override bool IsCompatibilityChecked {
            get {
                return isCompatibilityChecked.ContainsKey(ConnectionString) ? isCompatibilityChecked[ConnectionString] : false;
            }

            set {
                isCompatibilityChecked[ConnectionString] = value;
            }
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProviders.Add(new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, false), false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void RuntimeDbChooserWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
                e.Languages.Add(userLanguageName);
            }
        }
        private void RuntimeDbChooserWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
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
    }
}
