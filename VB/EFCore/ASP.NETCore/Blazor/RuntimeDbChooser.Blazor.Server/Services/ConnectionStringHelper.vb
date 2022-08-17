Imports Microsoft.Extensions.Configuration
Imports RuntimeDbChooser.Services
Imports System.Collections.Generic

Namespace RuntimeDbChooser.Blazor.Server.Services

    Public Class ConnectionStringHelper
        Implements RuntimeDbChooser.Services.IConnectionStringHelper

        Private ReadOnly configuration As IConfiguration

        Public Sub New(ByVal configuration As IConfiguration)
            Me.configuration = configuration
        End Sub

        Public Function GetConnectionStringsMap() As IDictionary(Of String, String)
            Dim connectionStrings As Dictionary(Of String, String) = New Dictionary(Of String, String)()
            Dim connectionsStr = configuration.GetSection("ConnectionStrings")
            For Each conf In connectionsStr.GetChildren()
                connectionStrings.Add(conf.Key, conf.Value)
            Next

            Return connectionStrings
        End Function
    End Class
End Namespace
