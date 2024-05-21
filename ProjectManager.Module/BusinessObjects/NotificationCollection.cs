using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;

namespace ProjectManager.Module.BusinessObjects
{
    [DefaultClassOptions]
    //make the default listing to be sorted by the notification date  in descending order
    [DefaultProperty(nameof(NotificationDate))]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class NotificationCollection : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public NotificationCollection(Session session)
            : base(session)
        {
            
        } 
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
        private string _NotificationText;
        public string NotificationText
        {
            get { return _NotificationText; }
            set { SetPropertyValue(nameof(NotificationText), ref _NotificationText, value); }
        }
        private DateTime _NotificationDate;
        public DateTime NotificationDate
        {
            get { return _NotificationDate; }
            set { SetPropertyValue(nameof(NotificationDate), ref _NotificationDate, value); }
        }
        private ApplicationUser _User;
        [Aggregated, Association("User-NotificationCollections")]
        public ApplicationUser User {
            get { return _User; }
            set { SetPropertyValue(nameof(User), ref _User, value); }

        }


        // assignd to user
        private ApplicationUser _AssignedTo;
        [Association("User-NotificationCollection")]
        public ApplicationUser AssignedTo
        {
            get { return _AssignedTo; }
            set { SetPropertyValue(nameof(AssignedTo), ref _AssignedTo, value); }
        }



    
    }
    public class NotificationCollectionCollectionService 
    {
        private IObjectSpace objectSpace;
        public NotificationCollectionCollectionService(IObjectSpace objectSpace)
        {
            this.objectSpace = objectSpace;
        }
        public XPCollection<NotificationCollection> GetNotificationsForCurrentUser(Session session)
        {
            // Get the username of the currently logged-in user
            string currentUserName = SecuritySystem.CurrentUserName;

            // Define criteria to filter notifications for the current user
            CriteriaOperator criteria = CriteriaOperator.Parse($"{nameof(NotificationCollection.AssignedTo)}.{nameof(ApplicationUser.UserName)} = ?", currentUserName);

            // Retrieve notifications for the current user from the database using the defined criteria
           XPCollection<NotificationCollection> notifications = new XPCollection<NotificationCollection>(session, criteria);


            return notifications;
        }

        internal XPCollection<NotificationCollection> GetNotificationsForCurrentUser(object session)
        {
            throw new NotImplementedException();
        }
    }


}