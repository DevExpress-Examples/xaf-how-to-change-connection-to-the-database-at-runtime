Imports System.Threading.Tasks
Imports DevExpress.ExpressApp.Blazor.Services
Imports Microsoft.AspNetCore.Connections
Imports Microsoft.AspNetCore.SignalR
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options

Namespace RuntimeDbChooser.Blazor.Server.Services

    Friend Class ProxyHubConnectionHandler(Of THub As Hub)
        Inherits HubConnectionHandler(Of THub)

        Private ReadOnly storageContainerInitializer As IValueManagerStorageContainerInitializer

        Public Sub New(ByVal lifetimeManager As HubLifetimeManager(Of THub), ByVal protocolResolver As IHubProtocolResolver, ByVal globalHubOptions As IOptions(Of HubOptions), ByVal hubOptions As IOptions(Of HubOptions(Of THub)), ByVal loggerFactory As ILoggerFactory, ByVal userIdProvider As IUserIdProvider, ByVal serviceScopeFactory As IServiceScopeFactory, ByVal storageContainerAccessor As IValueManagerStorageContainerInitializer)
            MyBase.New(lifetimeManager, protocolResolver, globalHubOptions, hubOptions, loggerFactory, userIdProvider, serviceScopeFactory)
            storageContainerInitializer = storageContainerAccessor
        End Sub

        Public Overrides Function OnConnectedAsync(ByVal connection As ConnectionContext) As Task
            storageContainerInitializer.Initialize()
            Return MyBase.OnConnectedAsync(connection)
        End Function
    End Class
End Namespace
