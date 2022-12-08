using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace RuntimeDbChooser.Module.BusinessObjects;
[CurrentUserDisplayImage(nameof(Photo))]
[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo, IXafEntityObject {
    private MediaDataObject photo;
    public ApplicationUser() : base() {
        UserLogins = new List<ApplicationUserLoginInfo>();
    }
    #region DEMO_REMOVE
    [JsonConstructor]
    public ApplicationUser(Guid ID) : base() {
        this.ID = ID;
    }
    #endregion
    [Browsable(false)]
    [DevExpress.ExpressApp.DC.Aggregated]
    public virtual IList<ApplicationUserLoginInfo> UserLogins { get; set; }
    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => UserLogins.OfType<ISecurityUserLoginInfo>();

    [VisibleInListView(false)]
    [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
    public virtual MediaDataObject Photo {
        get;set;
    }
    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }

  public override void OnCreated() {
        Photo = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<MediaDataObject>();
    }

}
