using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectManager.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class FeedBack : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public FeedBack(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            FeedBackFrom = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
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
        
        private string _FeedBackText;
        public string FeedBackText
        {
            get { return _FeedBackText; }
            set { SetPropertyValue(nameof(FeedBackText), ref _FeedBackText, value); }
        }
        private DateTime _FeedBackDate = DateTime.Today;
        public DateTime FeedBackDate
        {
            get { return _FeedBackDate; }
            set { SetPropertyValue(nameof(FeedBackDate), ref _FeedBackDate, value); }
        }
        //private DateTime _FeedBackTime =DateTime.Now;
        //public DateTime FeedBackTime
        //{
        //    get { return _FeedBackTime; }
        //    set { SetPropertyValue(nameof(FeedBackTime), ref _FeedBackTime, value); }
        //}
        private ApplicationUser _User;
        [Association("User-FeedBacks")]
        [VisibleInDetailView(false)]
        public ApplicationUser FeedBackFrom
        {
            //currently looged in user
            get
            {
               
                return _User;
            }
            set { SetPropertyValue(nameof(FeedBackFrom), ref _User, value); }

        }
        ////feedback to a user
        //private ApplicationUser _FeedBackTo;
        //[Association("User-FeedBackTo")]
        //[DataSourceProperty("FeedBackFrom")]

        //public ApplicationUser FeedBackTo
        //{
        //    get { return _FeedBackTo; }
        //    set { SetPropertyValue(nameof(FeedBackTo), ref _FeedBackTo, value); }
        //}


        //task
        private Task _Task;
        [Association("Task-FeedBacks")]
        public Task Task
        {
            get { return _Task; }
            set { SetPropertyValue(nameof(Task), ref _Task, value); }
        }
    }
}