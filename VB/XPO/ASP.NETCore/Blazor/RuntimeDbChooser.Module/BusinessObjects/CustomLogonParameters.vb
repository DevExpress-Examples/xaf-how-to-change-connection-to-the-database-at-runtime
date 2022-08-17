Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Core
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base
Imports Newtonsoft.Json
Imports RuntimeDbChooser.Services
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace RuntimeDbChooser.Module.BusinessObjects

    <DomainComponent>
    Public Class CustomLogonParametersForStandardAuthentication
        Inherits AuthenticationStandardLogonParameters
        Implements IDatabaseNameParameter, IServiceProviderConsumer

        Private serviceProvider As IServiceProvider

        Private dataBaseNameObj As DataBaseNameHolder

        <DataSourceProperty(NameOf(CustomLogonParametersForStandardAuthentication.GetDataBaseNames), DataSourcePropertyIsNullMode.SelectAll)>
        Public Property DatabaseName As DataBaseNameHolder Implements IDatabaseNameParameter.DatabaseName
            Get
                Return dataBaseNameObj
            End Get

            Set(ByVal value As DataBaseNameHolder)
                dataBaseNameObj = value
            End Set
        End Property

        Private dataBaseNameObjs As IReadOnlyList(Of DataBaseNameHolder)? = Nothing

        <Browsable(False)>
        <JsonIgnore>
        Public ReadOnly Property GetDataBaseNames As IReadOnlyList(Of DataBaseNameHolder)
            Get
                If dataBaseNameObjs Is Nothing Then
                    dataBaseNameObjs = New List(Of DataBaseNameHolder)()
                    Dim connectionStringHelper As IConnectionStringHelper = serviceProvider.GetRequiredService(Of IConnectionStringHelper)()
                    For Each dbname In connectionStringHelper.GetConnectionStringsMap().Keys
                        CType(dataBaseNameObjs, List(Of DataBaseNameHolder)).Add(New DataBaseNameHolder(dbname))
                    Next
                End If

                Return dataBaseNameObjs
            End Get
        End Property

        Public Sub SetServiceProvider(ByVal serviceProvider As IServiceProvider)
            Me.serviceProvider = serviceProvider
        End Sub
    End Class

    <DomainComponent>
    'set readonly using model editor
    '<DetailView Id = "DataBaseNameHolder_DetailView" AllowEdit="False" />
    'CustomLogonController checks that the DataBaseNameHolder property is not null
    Public Class DataBaseNameHolder
        Inherits NonPersistentLiteObject

        Public Sub New(ByVal name As String)
            Me.Name = name
        End Sub

        Public ReadOnly Property Name As String
    End Class
End Namespace
