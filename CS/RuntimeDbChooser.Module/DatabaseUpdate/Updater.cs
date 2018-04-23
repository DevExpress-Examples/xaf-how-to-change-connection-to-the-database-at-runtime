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

namespace RuntimeDbChooser.Module.DatabaseUpdate {
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
	public class Updater : ModuleUpdater {
		public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
			base(objectSpace, currentDBVersion) {
		}
		public override void UpdateDatabaseAfterUpdateSchema() {
			base.UpdateDatabaseAfterUpdateSchema();
			SecuritySystemUser userAdmin = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
			if(userAdmin == null) {
				userAdmin = ObjectSpace.CreateObject<SecuritySystemUser>();
				userAdmin.UserName = "Admin";
				// Set a password if the standard authentication type is used
				userAdmin.SetPassword("");
			}
			// If a role with the Administrators name doesn't exist in the database, create this role
			SecuritySystemRole adminRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
			if(adminRole == null) {
				adminRole = ObjectSpace.CreateObject<SecuritySystemRole>();
				adminRole.Name = "Administrators";
			}
			adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);

			if(ObjectSpace.Database.Contains("DB1")) {
				SecuritySystemUser sampleUser1 = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User1"));
				if(sampleUser1 == null) {
					sampleUser1 = ObjectSpace.CreateObject<SecuritySystemUser>();
					sampleUser1.UserName = "User1";
					sampleUser1.SetPassword("");
				}
				SecuritySystemRole defaultRole = CreateDefaultRole();
				sampleUser1.Roles.Add(defaultRole);
			}
			if(ObjectSpace.Database.Contains("DB2")) {
				SecuritySystemUser sampleUser2 = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User2"));
				if(sampleUser2 == null) {
					sampleUser2 = ObjectSpace.CreateObject<SecuritySystemUser>();
					sampleUser2.UserName = "User2";
					sampleUser2.SetPassword("");
				}
				SecuritySystemRole defaultRole = CreateDefaultRole();
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
		private SecuritySystemRole CreateDefaultRole() {
			SecuritySystemRole defaultRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Default"));
			if(defaultRole == null) {
				defaultRole = ObjectSpace.CreateObject<SecuritySystemRole>();
				defaultRole.Name = "Default";

				defaultRole.AddObjectAccessPermission<SecuritySystemUser>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
				defaultRole.AddMemberAccessPermission<SecuritySystemUser>("ChangePasswordOnFirstLogon", SecurityOperations.Write, "[Oid] = CurrentUserId()");
				defaultRole.AddMemberAccessPermission<SecuritySystemUser>("StoredPassword", SecurityOperations.Write, "[Oid] = CurrentUserId()");
				defaultRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.Read, SecuritySystemModifier.Allow);
				defaultRole.SetTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
				defaultRole.SetTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
			}
			return defaultRole;
		}
	}
}
