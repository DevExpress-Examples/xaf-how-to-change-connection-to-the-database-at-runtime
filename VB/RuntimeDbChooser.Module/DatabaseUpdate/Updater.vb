Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl

Namespace RuntimeDbChooser.Module.DatabaseUpdate
    ' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            Dim userAdmin As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "Admin"))
            If userAdmin Is Nothing Then
                userAdmin = ObjectSpace.CreateObject(Of SecuritySystemUser)()
                userAdmin.UserName = "Admin"
                ' Set a password if the standard authentication type is used
                userAdmin.SetPassword("")
            End If
            ' If a role with the Administrators name doesn't exist in the database, create this role
            Dim adminRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Administrators"))
            If adminRole Is Nothing Then
                adminRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
                adminRole.Name = "Administrators"
            End If
            adminRole.IsAdministrative = True
            userAdmin.Roles.Add(adminRole)

            If ObjectSpace.Database.Contains("DB1") Then
                Dim sampleUser1 As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "User1"))
                If sampleUser1 Is Nothing Then
                    sampleUser1 = ObjectSpace.CreateObject(Of SecuritySystemUser)()
                    sampleUser1.UserName = "User1"
                    sampleUser1.SetPassword("")
                End If
                Dim defaultRole As SecuritySystemRole = CreateDefaultRole()
                sampleUser1.Roles.Add(defaultRole)
            End If
            If ObjectSpace.Database.Contains("DB2") Then
                Dim sampleUser2 As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "User2"))
                If sampleUser2 Is Nothing Then
                    sampleUser2 = ObjectSpace.CreateObject(Of SecuritySystemUser)()
                    sampleUser2.UserName = "User2"
                    sampleUser2.SetPassword("")
                End If
                Dim defaultRole As SecuritySystemRole = CreateDefaultRole()
                sampleUser2.Roles.Add(defaultRole)
            End If
            ObjectSpace.CommitChanges()
        End Sub
        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
            'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            '}
        End Sub
        Private Function CreateDefaultRole() As SecuritySystemRole
            Dim defaultRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Default"))
            If defaultRole Is Nothing Then
                defaultRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
                defaultRole.Name = "Default"

                defaultRole.AddObjectAccessPermission(Of SecuritySystemUser)("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess)
                defaultRole.AddMemberAccessPermission(Of SecuritySystemUser)("ChangePasswordOnFirstLogon", SecurityOperations.Write, "[Oid] = CurrentUserId()")
                defaultRole.AddMemberAccessPermission(Of SecuritySystemUser)("StoredPassword", SecurityOperations.Write, "[Oid] = CurrentUserId()")
                defaultRole.SetTypePermissionsRecursively(Of SecuritySystemRole)(SecurityOperations.Read, SecuritySystemModifier.Allow)
                defaultRole.SetTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow)
                defaultRole.SetTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow)
            End If
            Return defaultRole
        End Function
    End Class
End Namespace
