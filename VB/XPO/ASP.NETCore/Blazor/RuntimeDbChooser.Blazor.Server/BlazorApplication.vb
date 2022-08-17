Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Blazor
Imports Microsoft.Extensions.DependencyInjection
Imports RuntimeDbChooser.Services
Imports System.Collections.Concurrent

Namespace RuntimeDbChooser.Blazor.Server

    Public Class RuntimeDbChooserBlazorApplication
        Inherits BlazorApplication

        Private Shared isCompatibilityCheckedField As ConcurrentDictionary(Of String, Boolean) = New ConcurrentDictionary(Of String, Boolean)()

        Protected Overrides Property IsCompatibilityChecked As Boolean
            Get
                Return isCompatibilityCheckedField.ContainsKey(ServiceProvider.GetRequiredService(Of RuntimeDbChooser.Services.IConnectionStringProvider)().GetConnectionString())
            End Get

            Set(ByVal value As Boolean)
                isCompatibilityCheckedField.TryAdd(ServiceProvider.GetRequiredService(Of RuntimeDbChooser.Services.IConnectionStringProvider)().GetConnectionString(), value)
            End Set
        End Property

        Protected Overrides Sub OnSetupStarted()
            MyBase.OnSetupStarted()
#If DEBUG
            If System.Diagnostics.Debugger.IsAttached AndAlso CheckCompatibilityType Is CheckCompatibilityType.DatabaseSchema Then
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
            End If
#End If
        End Sub

        Protected Overrides Function CreateLogonController() As LogonController
            Return CreateController(Of Controllers.CustomLogonController)()
        End Function
    End Class
End Namespace
