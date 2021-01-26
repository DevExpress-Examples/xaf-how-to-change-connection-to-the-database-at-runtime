Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.XtraEditors
Imports RuntimeDbChooser.Win

Public Class Program

    <STAThread()>
    Public Shared Sub Main(ByVal arguments() As String)
        DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.Latest
#If EASYTEST Then
              DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If
        WindowsFormsSettings.LoadApplicationSettings()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
		DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
        EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
        If Tracing.GetFileLocationFromSettings() = DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder Then
            Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath
        End If
        Tracing.Initialize()
        Dim _application As RuntimeDbChooserWindowsFormsApplication = New RuntimeDbChooserWindowsFormsApplication()
        _application.GetSecurityStrategy().RegisterXPOAdapterProviders()
        If (Not ConfigurationManager.ConnectionStrings.Item("ConnectionString") Is Nothing) Then
            _application.ConnectionString = ConfigurationManager.ConnectionStrings.Item("ConnectionString").ConnectionString
        End If
#If EASYTEST Then
        If (Not ConfigurationManager.ConnectionStrings.Item("EasyTestConnectionString") Is Nothing) Then
            _application.ConnectionString = ConfigurationManager.ConnectionStrings.Item("EasyTestConnectionString").ConnectionString
        End If
#End If
#If DEBUG Then
        If System.Diagnostics.Debugger.IsAttached AndAlso _application.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema Then
            _application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
        End If
#End If
        Try
            _application.Setup()
            _application.Start()
        Catch e As Exception
            _application.StopSplash()
            _application.HandleException(e)
        End Try

    End Sub
End Class