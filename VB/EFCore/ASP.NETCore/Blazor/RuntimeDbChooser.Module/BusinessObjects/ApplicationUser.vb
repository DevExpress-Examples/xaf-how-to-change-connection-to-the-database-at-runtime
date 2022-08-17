Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy

Namespace RuntimeDbChooser.Module.BusinessObjects

    <CurrentUserDisplayImage(NameOf(ApplicationUser.Photo))>
    <DefaultProperty(NameOf(PermissionPolicyUser.UserName))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements IObjectSpaceLink, ISecurityUserWithLoginInfo, IXafEntityObject

        Private photoField As MediaDataObject

        Public Sub New()
            MyBase.New()
            Me.UserLogins = New List(Of ApplicationUserLoginInfo)()
        End Sub

#Region "DEMO_REMOVE"
        <JsonConstructor>
        Public Sub New(ByVal ID As Integer)
            MyBase.New()
            Me.ID = ID
        End Sub

#End Region
        <Browsable(False)>
        <DC.Aggregated>
        Public Overridable Property UserLogins As IList(Of ApplicationUserLoginInfo)

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo)
            Get
                Return Me.UserLogins.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        <VisibleInListView(False)>
        <ImageEditor(ListViewImageEditorCustomHeight:=75, DetailViewImageEditorFixedHeight:=150)>
        Public Overridable Property Photo As MediaDataObject
            Get
                Return photoField
            End Get

            Set(ByVal value As MediaDataObject)
                SetReferencePropertyValue(photoField, value)
            End Set
        End Property

        Private Function CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = CType(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of ApplicationUserLoginInfo)()
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.UserProp = Me
            Return result
        End Function

        Public Sub OnCreated() Implements IXafEntityObject.OnCreated
            Photo = CType(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of MediaDataObject)()
        End Sub

        Public Sub OnSaving() Implements IXafEntityObject.OnSaving
        End Sub

        Public Sub OnLoaded() Implements IXafEntityObject.OnLoaded
        End Sub
    End Class
End Namespace
