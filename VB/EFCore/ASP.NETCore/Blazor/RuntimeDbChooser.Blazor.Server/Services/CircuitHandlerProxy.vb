Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.ExpressApp.Blazor.Services
Imports Microsoft.AspNetCore.Components.Server.Circuits

Namespace RuntimeDbChooser.Blazor.Server.Services

    Friend Class CircuitHandlerProxy
        Inherits CircuitHandler

        Private ReadOnly scopedCircuitHandler As IScopedCircuitHandler

        Public Sub New(ByVal scopedCircuitHandler As IScopedCircuitHandler)
            Me.scopedCircuitHandler = scopedCircuitHandler
        End Sub

        Public Overrides Function OnCircuitOpenedAsync(ByVal circuit As Circuit, ByVal cancellationToken As CancellationToken) As Task
            Return scopedCircuitHandler.OnCircuitOpenedAsync(cancellationToken)
        End Function

        Public Overrides Function OnConnectionUpAsync(ByVal circuit As Circuit, ByVal cancellationToken As CancellationToken) As Task
            Return scopedCircuitHandler.OnConnectionUpAsync(cancellationToken)
        End Function

        Public Overrides Function OnCircuitClosedAsync(ByVal circuit As Circuit, ByVal cancellationToken As CancellationToken) As Task
            Return scopedCircuitHandler.OnCircuitClosedAsync(cancellationToken)
        End Function

        Public Overrides Function OnConnectionDownAsync(ByVal circuit As Circuit, ByVal cancellationToken As CancellationToken) As Task
            Return scopedCircuitHandler.OnConnectionDownAsync(cancellationToken)
        End Function
    End Class
End Namespace
