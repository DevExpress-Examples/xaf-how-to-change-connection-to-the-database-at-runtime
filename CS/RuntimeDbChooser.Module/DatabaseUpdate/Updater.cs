using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace RuntimeDbChooser.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
		public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
			base(objectSpace, currentDBVersion) {
		}
		public override void UpdateDatabaseAfterUpdateSchema() {
			base.UpdateDatabaseAfterUpdateSchema();
			PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
			if(userAdmin == null) {
				userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
				userAdmin.UserName = "Admin";
				// Set a password if the standard authentication type is used
				userAdmin.SetPassword("");
			}
			// If a role with the Administrators name doesn't exist in the database, create this role
			PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
			if(adminRole == null) {
				adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
				adminRole.Name = "Administrators";
			}
			adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);

			if(ObjectSpace.Database.Contains("DB1")) {
				PermissionPolicyUser sampleUser1 = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User1"));
				if(sampleUser1 == null) {
					sampleUser1 = ObjectSpace.CreateObject<PermissionPolicyUser>();
					sampleUser1.UserName = "User1";
					sampleUser1.SetPassword("");
				}
				PermissionPolicyRole defaultRole = CreateDefaultRole();
				sampleUser1.Roles.Add(defaultRole);
			}
			if(ObjectSpace.Database.Contains("DB2")) {
				PermissionPolicyUser sampleUser2 = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User2"));
				if(sampleUser2 == null) {
					sampleUser2 = ObjectSpace.CreateObject<PermissionPolicyUser>();
					sampleUser2.UserName = "User2";
					sampleUser2.SetPassword("");
				}
				PermissionPolicyRole defaultRole = CreateDefaultRole();
				sampleUser2.Roles.Add(defaultRole);
			}
			ObjectSpace.CommitChanges();
		}
		public override void UpdateDatabaseBeforeUpdateSchema() {
			base.UpdateDatabaseBeforeUpdateSchema();
		}
		private PermissionPolicyRole CreateDefaultRole() {
			PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
			if(defaultRole == null) {
				defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
				defaultRole.Name = "Default";

                defaultRole.PermissionPolicy = SecurityPermissionPolicy.AllowAllByDefault;
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PermissionPolicyRole_ListView", SecurityPermissionState.Deny);
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PermissionPolicyUser_ListView", SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyUser>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PermissionPolicyTypePermissionObject>("Write;Delete;Create", SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyMemberPermissionsObject>("Write;Delete;Create", SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyObjectPermissionsObject>("Write;Delete;Create", SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyNavigationPermissionObject>("Write;Delete;Create", SecurityPermissionState.Deny);
                defaultRole.AddTypePermission<PermissionPolicyActionPermissionObject>("Write;Delete;Create", SecurityPermissionState.Deny);
            }
			return defaultRole;
		}
    }
}
