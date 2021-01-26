namespace RuntimeDbChooser.Blazor.Server {
    partial class RuntimeDbChooserBlazorApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule();
            this.module3 = new RuntimeDbChooser.Module.RuntimeDbChooserModule();
            this.module4 = new RuntimeDbChooser.Module.Blazor.RuntimeDbChooserBlazorModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationBlazorModule = new DevExpress.ExpressApp.Validation.Blazor.ValidationBlazorModule();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            // 
            // RuntimeDbChooserBlazorApplication
            // 
            this.ApplicationName = "RuntimeDbChooser";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.validationBlazorModule);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.RuntimeDbChooserBlazorApplication_DatabaseVersionMismatch);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule module2;
        private RuntimeDbChooser.Module.RuntimeDbChooserModule module3;
        private RuntimeDbChooser.Module.Blazor.RuntimeDbChooserBlazorModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Blazor.ValidationBlazorModule validationBlazorModule;
    }
}
