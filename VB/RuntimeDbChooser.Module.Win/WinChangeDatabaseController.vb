Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Module.Win
    Public Interface IApplicationFactory
        Function CreateApplication() As WinApplication
    End Interface

    Public Class WinChangeDatabaseController
        Inherits WindowController

        Public changeDatabaseAction As SingleChoiceAction
        Public Sub New()
            Me.TargetWindowType = WindowType.Main
            changeDatabaseAction = New SingleChoiceAction(Me, "ChangeDatabase", PredefinedCategory.View)
            For Each databaseName As String In MSSqlServerChangeDatabaseHelper.Databases.Split(";"c)
                changeDatabaseAction.Items.Add(New ChoiceActionItem(databaseName, databaseName))
            Next databaseName
            AddHandler changeDatabaseAction.Execute, AddressOf changeDatabaseAction_Execute
        End Sub


        Protected Overrides Sub OnActivated()
            MyBase.OnActivated()
            changeDatabaseAction.Active("ActiveOnlyForAdmin") = SecuritySystem.CurrentUserName = "Admin"
            For Each item As ChoiceActionItem In changeDatabaseAction.Items
                If Application.ConnectionString.Contains(CStr(item.Data)) Then
                    changeDatabaseAction.SelectedItem = item
                    Exit For
                End If
            Next item
            AddHandler Application.LoggedOn, AddressOf Application_LoggedOn
            AddHandler Application.LoggedOff, AddressOf Application_LoggedOff
        End Sub

        Private Sub Application_LoggedOff(ByVal sender As Object, ByVal e As EventArgs)
            If Not String.IsNullOrEmpty(WinChangeDatabaseHelper.DatabaseName) Then
                DirectCast(SecuritySystem.LogonParameters, IDatabaseNameParameter).DatabaseName = WinChangeDatabaseHelper.DatabaseName
            End If
            Dim authenticationStandardLogonParameters As AuthenticationStandardLogonParameters = TryCast(SecuritySystem.LogonParameters, AuthenticationStandardLogonParameters)
            If authenticationStandardLogonParameters IsNot Nothing AndAlso (Not String.IsNullOrEmpty(WinChangeDatabaseStandardAuthentication.AuthenticatedUserName)) Then
                authenticationStandardLogonParameters.UserName = WinChangeDatabaseStandardAuthentication.AuthenticatedUserName
            End If
        End Sub

        Private Sub Application_LoggedOn(ByVal sender As Object, ByVal e As LogonEventArgs)
            WinChangeDatabaseHelper.SkipLogonDialog = False
        End Sub
        Private Sub changeDatabaseAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)

            WinChangeDatabaseHelper.DatabaseName = CStr(e.SelectedChoiceActionItem.Data)
            WinChangeDatabaseHelper.SkipLogonDialog = True
            WinChangeDatabaseStandardAuthentication.AuthenticatedUserName = SecuritySystem.CurrentUserName

            Frame.GetController(Of LogoffController)().LogoffAction.DoExecute()
        End Sub
    End Class
End Namespace
