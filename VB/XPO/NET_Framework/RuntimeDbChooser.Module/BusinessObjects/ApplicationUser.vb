Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Xpo
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq

Namespace RuntimeDbChooser.Module.BusinessObjects

    <MapInheritance(MapInheritanceType.ParentTable)>
    <DefaultProperty(NameOf(PermissionPolicyUser.UserName))>
    <CurrentUserDisplayImage(NameOf(ApplicationUser.Photo))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements IObjectSpaceLink, ISecurityUserWithLoginInfo

        Private photoField As Byte()

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Browsable(False)>
        <Aggregated, Association("User-LoginInfo")>
        Public ReadOnly Property LoginInfo As XPCollection(Of ApplicationUserLoginInfo)
            Get
                Return GetCollection(Of ApplicationUserLoginInfo)(NameOf(ApplicationUser.LoginInfo))
            End Get
        End Property

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo) Implements IOAuthSecurityUser.UserLogins
            Get
                Return LoginInfo.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        Private Property ObjectSpace As IObjectSpace Implements IObjectSpaceLink.ObjectSpace

        Private Function CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = CType(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of ApplicationUserLoginInfo)()
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.UserProp = Me
            Return result
        End Function

        <VisibleInListView(False)>
        <ImageEditor(ListViewImageEditorCustomHeight:=75, DetailViewImageEditorFixedHeight:=150)>
        Public Property Photo As Byte()
            Get
                Return photoField
            End Get

            Set(ByVal value As Byte())
                SetPropertyValue(NameOf(ApplicationUser.Photo), photoField, value)
            End Set
        End Property
    End Class
End Namespace
