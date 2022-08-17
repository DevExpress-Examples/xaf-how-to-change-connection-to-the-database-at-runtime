Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

Namespace RuntimeDbChooser.Module.BusinessObjects

    <DeferredDeletion(False)>
    <Persistent("PermissionPolicyUserLoginInfo")>
    Public Class ApplicationUserLoginInfo
        Inherits BaseObject
        Implements ISecurityUserLoginInfo

        Private loginProviderNameField As String

        Private userField As ApplicationUser

        Private providerUserKeyField As String

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Indexed("ProviderUserKey", Unique:=True)>
        <Appearance("PasswordProvider", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.DefaultClaimsIssuer & "'", Context:="DetailView")>
        Public Property LoginProviderName As String Implements ISecurityUserLoginInfo.LoginProviderName
            Get
                Return loginProviderNameField
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(ApplicationUserLoginInfo.LoginProviderName), loginProviderNameField, value)
            End Set
        End Property

        <Appearance("PasswordProviderUserKey", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.DefaultClaimsIssuer & "'", Context:="DetailView")>
        Public Property ProviderUserKey As String Implements ISecurityUserLoginInfo.ProviderUserKey
            Get
                Return providerUserKeyField
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(ApplicationUserLoginInfo.ProviderUserKey), providerUserKeyField, value)
            End Set
        End Property

        <Association("User-LoginInfo")>
        Public Property UserProp As ApplicationUser
            Get
                Return userField
            End Get

            Set(ByVal value As ApplicationUser)
                SetPropertyValue(NameOf(ApplicationUserLoginInfo.UserProp), userField, value)
            End Set
        End Property

        Private ReadOnly Property User As Object Implements ISecurityUserLoginInfo.User
            Get
                Return UserProp
            End Get
        End Property
    End Class
End Namespace
