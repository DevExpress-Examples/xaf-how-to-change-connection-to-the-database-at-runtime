using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace RuntimeDbChooser.Module.DatabaseUpdate {
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
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
			//if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
			//    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
			//}
		}
		private PermissionPolicyRole CreateDefaultRole() {
			PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
			if(defaultRole == null) {
				defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
				defaultRole.Name = "Default";

                defaultRole.AddTypePermissionsRecursively<PermissionPolicyUser>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.FullAccess, SecurityPermissionState.Deny);
                defaultRole.SetTypePermission<PermissionPolicyTypePermissionObject>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.ReadOnlyAccess, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                //  userRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/Contact_ListView", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", null, SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", null, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<AuditDataItemPersistent>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
            }
			return defaultRole;
		}
	}
}
