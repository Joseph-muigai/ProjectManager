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

namespace ProjectManager.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Comment : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Comment(Session session)
            : base(session)
        {

        }
        public void commentedBy()
        {
            CommentedBy = SecuritySystem.CurrentUser as ApplicationUser;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CommentedBy = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
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
        private string _CommentText;
        public string CommentText
        {
            get { return _CommentText; }
            set { SetPropertyValue(nameof(CommentText), ref _CommentText, value); }
        }
        private DateTime _CommentDate;
        public DateTime CommentDate
        {
            get { return _CommentDate; }
            set { SetPropertyValue(nameof(CommentDate), ref _CommentDate, value); }
        }
        private ApplicationUser _CommentedBy;

        [Association("User-Comment")]

        public ApplicationUser CommentedBy
        {
            get
            {
                
                return _CommentedBy;
            }
            set { SetPropertyValue(nameof(CommentedBy), ref _CommentedBy, value); }
        }
        private Task _Task;
        [Association("Task-Comments")]
        public Task Task
        {
            get { return _Task; }
            set { SetPropertyValue(nameof(Task), ref _Task, value); }
        }
        //comment reply
        //private Comment _ReplyTo;
        //[Association("Comment-Reply")]
        //public Comment ReplyTo
        //{
        //    get { return _ReplyTo; }
        //    set { SetPropertyValue(nameof(ReplyTo), ref _ReplyTo, value); }
        //}

    }
}