Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports RuntimeDbChooser.Module.BusinessObjects

Namespace RuntimeDbChooser.Module.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ' If a user named 'Sam' doesn't exist in the database, create this user
            Dim userAdmin As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u) Equals(u.UserName, "Admin"))
            If userAdmin Is Nothing Then
                userAdmin = ObjectSpace.CreateObject(Of ApplicationUser)()
                userAdmin.UserName = "Admin"
                ' Set a password if the standard authentication type is used
                userAdmin.SetPassword("")
                ' The UserLoginInfo object requires a user object Id (Oid).
                ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges() 'This line persists created object(s).
                CType(userAdmin, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin))
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
                Dim sampleUser1 As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u) Equals(u.UserName, "User1"))
                If sampleUser1 Is Nothing Then
                    sampleUser1 = ObjectSpace.CreateObject(Of ApplicationUser)()
                    sampleUser1.UserName = "User1"
                    ' Set a password if the standard authentication type is used
                    sampleUser1.SetPassword("")
                    ' The UserLoginInfo object requires a user object Id (Oid).
                    ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                    ObjectSpace.CommitChanges() 'This line persists created object(s).
                    CType(sampleUser1, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser1))
                End If

                Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
                sampleUser1.Roles.Add(defaultRole)
            End If

            If ObjectSpace.Database.Contains("DB2") Then
                Dim sampleUser2 As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u) Equals(u.UserName, "User2"))
                If sampleUser2 Is Nothing Then
                    sampleUser2 = ObjectSpace.CreateObject(Of ApplicationUser)()
                    sampleUser2.UserName = "User2"
                    ' Set a password if the standard authentication type is used
                    sampleUser2.SetPassword("")
                    ' The UserLoginInfo object requires a user object Id (Oid).
                    ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                    ObjectSpace.CommitChanges() 'This line persists created object(s).
                    CType(sampleUser2, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser2))
                End If

                Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
                sampleUser2.Roles.Add(defaultRole)
            End If

            ObjectSpace.CommitChanges()
        End Sub

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
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
End Namespace
