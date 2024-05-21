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
    public class Project : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Project(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            //cra
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
       
        private string _ProjectName;
        public string ProjectName
        {
            get { return _ProjectName; }
            set { SetPropertyValue(nameof(ProjectName), ref _ProjectName, value); }
        }
        private string _ProjectDescription;
        [Size(SizeAttribute.Unlimited)]
        public string ProjectDescription
        {
            get { return _ProjectDescription; }
            set { SetPropertyValue(nameof(ProjectDescription), ref _ProjectDescription, value); }
        }
        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { SetPropertyValue(nameof(StartDate), ref _StartDate, value); }
        }
        private DateTime _EndDate;
        public DateTime DueDate
        {
            get { return _EndDate; }
            set { SetPropertyValue(nameof(DueDate), ref _EndDate, value); }
        }
        private Status _Status;
        public Status Status
        {
            get { return _Status; }
            set { SetPropertyValue(nameof(Status), ref _Status, value); }
        }
        private Priority _Priority;
        public Priority Priority
        {
            get { return _Priority; }
            set { SetPropertyValue(nameof(Priority), ref _Priority, value); }
        }
        //private string _ProjectManager;
        //[Association("Project-Member")]
        //public string ProjectManager
        //{
        //    get { return _ProjectManager; }
        //    set { SetPropertyValue(nameof(ProjectManager), ref _ProjectManager, value); }
        //}
        [Association("Project-Tasks")]
        public XPCollection<Task> Tasks
        {
            get { return GetCollection<Task>(nameof(Tasks)); }
        }
        [Association("ApplicationUser-Projects")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public XPCollection<ApplicationUser> ApplicationUser
        {
            get { return GetCollection<ApplicationUser>(nameof(ApplicationUser)); }
        }
        // milestones
        [Association("Project-Milestones")]
        public XPCollection<Milestone> Milestones
        {
            get { return GetCollection<Milestone>(nameof(Milestones)); }
        }
        //teams
        private Team _team;
        [Association("Project-Team")]
        public Team Team
        {
            get { return _team; }
            set { SetPropertyValue(nameof(Team), ref _team, value); }
        }

        //project resources
        [Association("Project-Resources")]
        public XPCollection<ProjectResource> Resources
        {
            get { return GetCollection<ProjectResource>(nameof(Resources)); }
        }

        //-------------------------------------------------------------------------------------
        [Association("Project-Calendar")]
        public XPCollection<Calendar> Calendars
        {

            //display calendar events for the project
            get { return GetCollection<Calendar>(nameof(Calendars)); }
        }

        //-------------------------------------------------------------------------------------


    }
}