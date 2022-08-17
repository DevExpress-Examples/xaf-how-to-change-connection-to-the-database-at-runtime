Imports System
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Runtime.CompilerServices
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Security

Namespace RuntimeDbChooser.[Module].BusinessObjects

    <Table("PermissionPolicyUserLoginInfo")>
    Public Class ApplicationUserLoginInfo
        Implements DevExpress.ExpressApp.IObjectSpaceLink, System.ComponentModel.INotifyPropertyChanged, DevExpress.ExpressApp.Security.ISecurityUserLoginInfo

        Private _ID As Int32

        Private loginProviderNameField As String

        Private providerUserKeyField As String

        Private userField As RuntimeDbChooser.[Module].BusinessObjects.ApplicationUser

        Public Sub New()
        End Sub

        <System.ComponentModel.BrowsableAttribute(False)>
        Public Property ID As Int32
            Get
                Return _ID
            End Get

            Protected Set(ByVal value As Int32)
                _ID = value
            End Set
        End Property

        <Appearance("PasswordProvider", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & DevExpress.ExpressApp.Security.SecurityDefaults.DefaultClaimsIssuer & "'", Context:="DetailView")>
        Public Property LoginProviderName As String Implements Global.DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName
            Get
                Return Me.loginProviderNameField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.loginProviderNameField, value) Then
                    Me.loginProviderNameField = value
                    Me.OnPropertyChanged()
                End If
            End Set
        End Property

        <Appearance("PasswordProviderUserKey", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & DevExpress.ExpressApp.Security.SecurityDefaults.DefaultClaimsIssuer & "'", Context:="DetailView")>
        Public Property ProviderUserKey As String Implements Global.DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey
            Get
                Return Me.providerUserKeyField
            End Get

            Set(ByVal value As String)
                If Not Equals(Me.providerUserKeyField, value) Then
                    Me.providerUserKeyField = value
                    Me.OnPropertyChanged()
                End If
            End Set
        End Property

        <System.ComponentModel.BrowsableAttribute(False)>
        Public Property UserForeignKey As Integer

        <Required>
        <ForeignKey(NameOf(RuntimeDbChooser.[Module].BusinessObjects.ApplicationUserLoginInfo.UserForeignKey))>
        Public Overridable Property UserProp As ApplicationUser
            Get
                Return Me.userField
            End Get

            Set(ByVal value As ApplicationUser)
                If Not System.[Object].Equals(Me.userField, value) Then
                    Me.userField = value
                    Me.OnPropertyChanged()
                End If
            End Set
        End Property

        Private ReadOnly Property User As Object Implements Global.DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.User
            Get
                Return Me.UserProp
            End Get
        End Property

#Region "IObjectSpaceLink members"
        Private objectSpaceField As DevExpress.ExpressApp.IObjectSpace

        Private Property ObjectSpace As IObjectSpace Implements Global.DevExpress.ExpressApp.IObjectSpaceLink.ObjectSpace
            Get
                Return Me.objectSpaceField
            End Get

            Set(ByVal value As IObjectSpace)
                Me.objectSpaceField = value
            End Set
        End Property

#End Region
#Region "INotifyPropertyChanged members"
        Private Sub OnPropertyChanged(<System.Runtime.CompilerServices.CallerMemberNameAttribute> ByVal Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
        End Sub

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements Global.System.ComponentModel.INotifyPropertyChanged.PropertyChanged
#End Region
    End Class
End Namespace
