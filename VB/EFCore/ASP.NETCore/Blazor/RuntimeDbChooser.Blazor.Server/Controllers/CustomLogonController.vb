Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Blazor
Imports DevExpress.ExpressApp.Blazor.SystemModule
Imports DevExpress.ExpressApp.Core
Imports DevExpress.ExpressApp.Security
Imports Microsoft.Extensions.DependencyInjection
Imports RuntimeDbChooser.Services

Namespace RuntimeDbChooser.Blazor.Server.Controllers

    Public Class CustomLogonController
        Inherits BlazorLogonController

        Protected Overrides Sub Accept(ByVal args As SimpleActionExecuteEventArgs)
            Dim targetDataBaseName As String? = CType(Application, BlazorApplication).ServiceProvider.GetRequiredService(Of ILogonParameterProvider)().GetLogonParameters(Of RuntimeDbChooser.Services.IDatabaseNameParameter)().DatabaseName?.Name
            If String.IsNullOrEmpty(targetDataBaseName) Then
                Throw New UserFriendlyException("The DatabaseName is not specified!")
            End If

            CType(Application, BlazorApplication).ServiceProvider.GetRequiredService(Of IObjectSpaceProviderContainer)().Clear()
            MyBase.Accept(args)
        End Sub
    End Class
End Namespace
