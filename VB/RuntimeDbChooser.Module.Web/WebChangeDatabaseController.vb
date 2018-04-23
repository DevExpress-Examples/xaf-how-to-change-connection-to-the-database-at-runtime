Imports System
Imports System.Web
Imports System.Web.Security
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Actions
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Module.Web
    Public Class WebChangeDatabaseController
        Inherits WindowController

        Public Const DatabaseParameterName As String = "DatabaseName"
        Public changeDatabaseAction As SingleChoiceAction
        Public Sub New()
            TargetWindowType = WindowType.Main

            changeDatabaseAction = New SingleChoiceAction(Me, "ChangeDatabase", "Security")
            For Each databaseName As String In MSSqlServerChangeDatabaseHelper.Databases.Split(";"c)
                changeDatabaseAction.Items.Add(New ChoiceActionItem(databaseName, databaseName))
            Next databaseName
            AddHandler changeDatabaseAction.Execute, AddressOf changeDatabaseAction_Execute
        End Sub

        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            For Each item As ChoiceActionItem In changeDatabaseAction.Items
                If Application.ConnectionString.Contains(CStr(item.Data)) Then
                    changeDatabaseAction.SelectedItem = item
                    Exit For
                End If
            Next item
        End Sub
        Private Sub changeDatabaseAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
            SecuritySystem.Instance.Logoff()
            HttpContext.Current.Session.Abandon()
            WebApplication.Redirect(String.Format("{0}?{1}={2}", FormsAuthentication.DefaultUrl, DatabaseParameterName, CStr(e.SelectedChoiceActionItem.Data)))
        End Sub
    End Class
End Namespace
