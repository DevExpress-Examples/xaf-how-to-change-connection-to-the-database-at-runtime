Imports DevExpress.ExpressApp.Security
Imports RuntimeDbChooser.Services
Imports System.Linq

Namespace RuntimeDbChooser.Blazor.Server.Services

    Public Class ConnectionStringProvider
        Implements RuntimeDbChooser.Services.IConnectionStringProvider

        Private ReadOnly logonParameterProvider As ILogonParameterProvider

        Private ReadOnly connectionStringHelper As RuntimeDbChooser.Services.IConnectionStringHelper

        Public Sub New(ByVal logonParameterProvider As ILogonParameterProvider, ByVal connectionStringHelper As RuntimeDbChooser.Services.IConnectionStringHelper)
            Me.logonParameterProvider = logonParameterProvider
            Me.connectionStringHelper = connectionStringHelper
        End Sub

        Public Function GetConnectionString() As String Implements RuntimeDbChooser.Services.IConnectionStringProvider.GetConnectionString
            'Configure the connection string based on logon parameter values.
            Dim targetDataBaseName As String? = logonParameterProvider.GetLogonParameters(Of RuntimeDbChooser.Services.IDatabaseNameParameter)().DatabaseName?.Name
            If targetDataBaseName IsNot Nothing Then
                Return connectionStringHelper.GetConnectionStringsMap()(targetDataBaseName)
            End If

            Return connectionStringHelper.GetConnectionStringsMap().Values.First()
        End Function
    End Class
End Namespace
