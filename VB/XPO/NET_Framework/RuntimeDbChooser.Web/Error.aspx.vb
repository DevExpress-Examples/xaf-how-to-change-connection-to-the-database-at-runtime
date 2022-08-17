Imports System
Imports System.Web.UI
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.SystemModule
Imports DevExpress.ExpressApp.Web.Templates
Imports DevExpress.ExpressApp.Web.TestScripts

Public Partial Class ErrorPage
    Inherits Page

    Protected Overrides Sub InitializeCulture()
        If WebApplication.Instance IsNot Nothing Then Call WebApplication.Instance.InitializeCulture()
    End Sub

    Protected Overrides Sub OnPreInit(ByVal e As EventArgs)
        MyBase.OnPreInit(e)
        Call BaseXafPage.SetupCurrentTheme()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If TestScriptsManager.EasyTestEnabled Then
            Dim testScriptsManager As TestScriptsManager = New TestScriptsManager(Page)
            testScriptsManager.RegisterControl(JSLabelTestControl.ClassName, "FormCaption", TestControlType.Field, "FormCaption")
            testScriptsManager.RegisterControl(JSLabelTestControl.ClassName, "DescriptionTextBox", TestControlType.Field, "Description")
            testScriptsManager.RegisterControl(JSDefaultTestControl.ClassName, "ReportButton", TestControlType.Action, "Report")
            testScriptsManager.AllControlRegistered()
            ClientScript.RegisterStartupScript([GetType](), "EasyTest", testScriptsManager.GetScript(), True)
        End If

        If WebApplication.Instance IsNot Nothing Then
            ApplicationTitle.Text = WebApplication.Instance.Title
        Else
            ApplicationTitle.Text = "No application"
        End If

        Header.Title = "Application Error - " & ApplicationTitle.Text
        Dim errorInfo As ErrorInfo = ErrorHandling.GetApplicationError()
        If errorInfo IsNot Nothing Then
            If ErrorHandling.CanShowDetailedInformation Then
                DetailsText.Text = errorInfo.GetTextualPresentation(True)
            Else
                Details.Visible = False
            End If

            ReportForm.Visible = ErrorHandling.CanSendAlertToAdmin
        Else
            ErrorPanel.Visible = False
        End If
    End Sub

#Region "Web Form Designer generated code"
    Overrides Protected Sub OnInit(ByVal e As EventArgs)
        InitializeComponent()
        MyBase.OnInit(e)
    End Sub

    Private Sub InitializeComponent()
        AddHandler Load, New EventHandler(AddressOf Page_Load)
        AddHandler PreRender, New EventHandler(AddressOf ErrorPage_PreRender)
    End Sub

    Private Sub ErrorPage_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Call RegisterThemeAssemblyController.RegisterThemeResources(CType(sender, Page))
    End Sub

#End Region
    Protected Sub ReportButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim errorInfo As ErrorInfo = ErrorHandling.GetApplicationError()
        If errorInfo IsNot Nothing Then
            ErrorHandling.SendAlertToAdmin(errorInfo.Id, DescriptionTextBox.Text, errorInfo.Exception.Message)
            ClientScript.RegisterStartupScript([GetType](), "alert", "alert('Your report has been sent. Thank you.');", True)
        End If
    End Sub

    Protected Sub NavigateToStart_Click(ByVal sender As Object, ByVal e As EventArgs)
        Call WebApplication.Instance.LogOff()
    End Sub
End Class
