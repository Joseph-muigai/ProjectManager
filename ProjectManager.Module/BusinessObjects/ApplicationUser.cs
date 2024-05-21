using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Notifications;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Utils;
using DevExpress.Xpo;

namespace ProjectManager.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
[CurrentUserDisplayImage(nameof(Photo))]

//include notification interface
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout
{
    private int accessFailedCount;
    private DateTime lockoutEnd;

    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    public int AccessFailedCount {
        get { return accessFailedCount; }
        set { SetPropertyValue(nameof(AccessFailedCount), ref accessFailedCount, value); }
    }

    [Browsable(false)]
    public DateTime LockoutEnd {
        get { return lockoutEnd; }
        set { SetPropertyValue(nameof(LockoutEnd), ref lockoutEnd, value); }
    }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }


    /// <summary>
    /// the 
    /// </summary>
    ///


    private string _FirstName;
    public string FirstName
    {
        get { return _FirstName; }
        set { SetPropertyValue(nameof(FirstName), ref _FirstName, value); }
    }
    private string _LastName;
    public string LastName
    {
        get { return _LastName; }
        set { SetPropertyValue(nameof(LastName), ref _LastName, value); }
    }
    private string _Email;
    public string Email
    {
        get { return _Email; }
        set { SetPropertyValue(nameof(Email), ref _Email, value); }
    }
    private string _Phone;
    public string Phone
    {
        get { return _Phone; }
        set { SetPropertyValue(nameof(Phone), ref _Phone, value); }
    }

    private string _Address;
    public string Address
    {
        get { return _Address; }
        set { SetPropertyValue(nameof(Address), ref _Address, value); }
    }

    private MediaDataObject photo;
    [VisibleInListView(false)]
    public  MediaDataObject Photo
    {
        get { return photo; }
        set { SetPropertyValue(nameof(Photo), ref photo, value); }

    }

    [Aggregated, Association("User-NotificationCollections")]
    public XPCollection<NotificationCollection> NotificationCollections1
    {
        get { return GetCollection<NotificationCollection>(nameof(NotificationCollections1)); }
    }





    [Association("ApplicationUser-Projects")]
    public XPCollection<Project> Projects
    {
        get { return GetCollection<Project>(nameof(Projects)); }
    }

    [Association("Tasks-ApplicationUser")]
    public XPCollection<Task> Tasks
    {
        get { return GetCollection<Task>(nameof(Tasks)); }
    }
     
    

    [Association("Teams-ApplicationUsers")]
    public XPCollection<Team> Teams
    {
        get { return GetCollection<Team>(nameof(Teams)); }
    }

    [VisibleInListView(false)]
    [Association("User-Comment")]
    public XPCollection<Comment> Comments
    {
        get { return GetCollection<Comment>(nameof(Comments)); }
    }
    //[Association("Tasks-ApplicationUser")]
    //public XPCollection<Task> AssignedTasks
    //{
    //    get { return GetCollection<Task>(nameof(AssignedTasks)); }
    //}


    public string DisplayMemberNameForLookupEditorsOfThisType
    {
        get { return string.Format("{0} {1}", FirstName, LastName); }
    }
    public override string ToString()
    {
        return DisplayMemberNameForLookupEditorsOfThisType;
    }
    [Association("User-NotificationCollection")]
    public XPCollection<NotificationCollection> NotificationCollections
    {
        get { return GetCollection<NotificationCollection>(nameof(NotificationCollections)); }
    }
    [Association("User-FeedBacks")]
    public XPCollection<FeedBack> FeedBacks
    {
        get { return GetCollection<FeedBack>(nameof(FeedBacks)); }
    }
    //[Association("User-FeedBackTo")]
    //public XPCollection<FeedBack> FeedBackTo
    //{
    //    get { return GetCollection<FeedBack>(nameof(FeedBackTo)); }
    //}



    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
    public void Notify(string message)
    {
        // Create a new notification


    }


}
