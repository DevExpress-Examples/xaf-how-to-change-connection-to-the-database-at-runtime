using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Security;

namespace RuntimeDbChooser.Module.BusinessObjects;
[Table("PermissionPolicyUserLoginInfo")]
public class ApplicationUserLoginInfo : IObjectSpaceLink, INotifyPropertyChanged, ISecurityUserLoginInfo {
    private string loginProviderName;
    private string providerUserKey;
    private ApplicationUser user;

    public ApplicationUserLoginInfo() { }

    [Browsable(false)]
    public Int32 ID { get; protected set; }

    [Appearance("PasswordProvider", Enabled = false, Criteria = "!(IsNewObject(this)) and LoginProviderName == '" + SecurityDefaults.DefaultClaimsIssuer + "'", Context = "DetailView")]
    public string LoginProviderName {
        get { return loginProviderName; }
        set {
            if(loginProviderName != value) {
                loginProviderName = value;
                OnPropertyChanged();
            }
        }
    }

    [Appearance("PasswordProviderUserKey", Enabled = false, Criteria = "!(IsNewObject(this)) and LoginProviderName == '" + SecurityDefaults.DefaultClaimsIssuer + "'", Context = "DetailView")]
    public string ProviderUserKey {
        get { return providerUserKey; }
        set {
            if(providerUserKey != value) {
                providerUserKey = value;
                OnPropertyChanged();
            }
        }
    }

    [Browsable(false)]
    public int UserForeignKey { get; set; }

    [Required]
    [ForeignKey(nameof(UserForeignKey))]
    public virtual ApplicationUser User {
        get { return user; }
        set {
            if(!Equals(user, value)) {
                user = value;
                OnPropertyChanged();
            }
        }
    }
    object ISecurityUserLoginInfo.User => User;

    #region IObjectSpaceLink members
    private IObjectSpace objectSpace;
    IObjectSpace IObjectSpaceLink.ObjectSpace {
        get { return objectSpace; }
        set { objectSpace = value; }
    }
    #endregion

    #region INotifyPropertyChanged members
    private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion
}
