Namespace RuntimeDbChooser.Win
    Partial Public Class RuntimeDbChooserWindowsFormsApplication
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.module3 = New RuntimeDbChooser.Module.RuntimeDbChooserModule()
            Me.module4 = New RuntimeDbChooser.Module.Win.RuntimeDbChooserWindowsFormsModule()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
            Me.validationWindowsFormsModule = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
            Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
            DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemRole)
            Me.securityStrategyComplex1.UserType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)
            ' 
            ' securityModule1
            ' 
            Me.securityModule1.UserType = GetType(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)
            ' 
            ' authenticationStandard1
            ' 
            Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
            ' 
            ' RuntimeDbChooserWindowsFormsApplication
            ' 
            Me.ApplicationName = "RuntimeDbChooser"
            Me.LinkNewObjectToParentImmediately = False
            Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.module4)
            Me.Modules.Add(Me.securityModule1)
            Me.Security = Me.securityStrategyComplex1
            Me.Modules.Add(Me.validationModule)
            Me.Modules.Add(Me.validationWindowsFormsModule)
            Me.UseOldTemplates = False

            DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        #End Region

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
        Private module3 As RuntimeDbChooser.Module.RuntimeDbChooserModule
        Private module4 As RuntimeDbChooser.Module.Win.RuntimeDbChooserWindowsFormsModule
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
        Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
        Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
        Private validationWindowsFormsModule As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
    End Class
End Namespace
