Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
Public Class Updater
    Inherits ModuleUpdater
    Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
        MyBase.New(objectSpace, currentDBVersion)
    End Sub

    Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
        MyBase.UpdateDatabaseAfterUpdateSchema()
        Dim userAdmin As PermissionPolicyUser = ObjectSpace.FindObject(Of PermissionPolicyUser)(New BinaryOperator("UserName", "Admin"))
        If userAdmin Is Nothing Then
            userAdmin = ObjectSpace.CreateObject(Of PermissionPolicyUser)()
            userAdmin.UserName = "Admin"
            ' Set a password if the standard authentication type is used
            userAdmin.SetPassword("")
        End If
        ' If a role with the Administrators name doesn't exist in the database, create this role
        Dim adminRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Administrators"))
        If adminRole Is Nothing Then
            adminRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
            adminRole.Name = "Administrators"
        End If
        adminRole.IsAdministrative = True
        userAdmin.Roles.Add(adminRole)

        If ObjectSpace.Database.Contains("DB1") Then
            Dim sampleUser1 As PermissionPolicyUser = ObjectSpace.FindObject(Of PermissionPolicyUser)(New BinaryOperator("UserName", "User1"))
            If sampleUser1 Is Nothing Then
                sampleUser1 = ObjectSpace.CreateObject(Of PermissionPolicyUser)()
                sampleUser1.UserName = "User1"
                sampleUser1.SetPassword("")
            End If
            Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
            sampleUser1.Roles.Add(defaultRole)
        End If
        If ObjectSpace.Database.Contains("DB2") Then
            Dim sampleUser2 As PermissionPolicyUser = ObjectSpace.FindObject(Of PermissionPolicyUser)(New BinaryOperator("UserName", "User2"))
            If sampleUser2 Is Nothing Then
                sampleUser2 = ObjectSpace.CreateObject(Of PermissionPolicyUser)()
                sampleUser2.UserName = "User2"
                sampleUser2.SetPassword("")
            End If
            Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
            sampleUser2.Roles.Add(defaultRole)
        End If
        ObjectSpace.CommitChanges()
    End Sub

    Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
        MyBase.UpdateDatabaseBeforeUpdateSchema()
        'If (CurrentDBVersion < New Version("1.1.0.0") AndAlso CurrentDBVersion > New Version("0.0.0.0")) Then
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName")
        'End If
    End Sub
    Private Function CreateDefaultRole() As PermissionPolicyRole
        Dim defaultRole As PermissionPolicyRole = ObjectSpace.FindObject(Of PermissionPolicyRole)(New BinaryOperator("Name", "Default"))

        If defaultRole Is Nothing Then
            defaultRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
            defaultRole.Name = "Default"
            defaultRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault
            defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PermissionPolicyRole_ListView", SecurityPermissionState.Deny)
            defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PermissionPolicyUser_ListView", SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyRole)(SecurityOperations.FullAccess, SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyUser)(SecurityOperations.FullAccess, SecurityPermissionState.Deny)
            defaultRole.AddObjectPermission(Of PermissionPolicyUser)(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow)
            defaultRole.AddMemberPermission(Of PermissionPolicyUser)(SecurityOperations.Write, "ChangePasswordOnFirstLogon", Nothing, SecurityPermissionState.Allow)
            defaultRole.AddMemberPermission(Of PermissionPolicyUser)(SecurityOperations.Write, "StoredPassword", Nothing, SecurityPermissionState.Allow)
            defaultRole.AddTypePermission(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Allow)
            defaultRole.AddTypePermission(Of PermissionPolicyTypePermissionObject)("Write;Delete;Create", SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyMemberPermissionsObject)("Write;Delete;Create", SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyObjectPermissionsObject)("Write;Delete;Create", SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyNavigationPermissionObject)("Write;Delete;Create", SecurityPermissionState.Deny)
            defaultRole.AddTypePermission(Of PermissionPolicyActionPermissionObject)("Write;Delete;Create", SecurityPermissionState.Deny)
        End If

        Return defaultRole
    End Function
End Class