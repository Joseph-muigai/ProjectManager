using DevExpress.Data.Filtering;

using DevExpress.Diagram.Core;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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

    [Appearance("MilestoneCompleted", TargetItems = " * ",
    Context = "ListView", Criteria = "IsCompleted = true", FontColor = "Green" )]
    [RuleCriteria("milestone not in the past", DefaultContexts.Save, "MilestoneDate >= Today", "Milestone date should be greater than or equal to today")]
    //start date  cannot be earlier than the start date of the assigned project
    [RuleCriteria("msStartDateNotBeforeProjectStartDate", DefaultContexts.Save, "MilestoneDate >= Project.StartDate",  "Milestone date should be greater than or equal to the project start date")]
    //end date cannot be later than the end date of the assigned project
    [RuleCriteria("msEndDateNotAfterProjectEndDate", DefaultContexts.Save, "MilestoneEndDate <= Project.EndDate", "Milestone end date should be less than or equal to the project end date")]
    //milestone end date cannot be earlier than the milestone start date
    [RuleCriteria("msEndDateNotBeforeStartDate", DefaultContexts.Save, "MilestoneEndDate >= MilestoneDate", "Milestone end date should be greater than or equal to the milestone start date")]
    


    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Milestone : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.      
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Milestone(Session session)
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

        private string _MilestoneName;
        public string MilestoneName
        {
            get { return _MilestoneName; }
            set { SetPropertyValue(nameof(MilestoneName), ref _MilestoneName, value); }
        }
        private string _MilestoneDescription;
        public string MilestoneDescription
        {
            get { return _MilestoneDescription; }
            set { SetPropertyValue(nameof(MilestoneDescription), ref _MilestoneDescription, value); }
        }
        private DateTime _MilestoneDate;
      
        public DateTime MilestoneDate
        {
            get { return _MilestoneDate; }
            set { SetPropertyValue(nameof(MilestoneDate), ref _MilestoneDate, value); }
        }
        private DateTime _MilestoneEndDate;
        public DateTime MilestoneEndDate
        {
            get { return _MilestoneEndDate; }
            set { SetPropertyValue(nameof(MilestoneEndDate), ref _MilestoneEndDate, value); }
        }
        private bool _IsCompleted;
        [ImmediatePostData]
        public bool IsCompleted
        {
            get { return _IsCompleted; }
            set { SetPropertyValue(nameof(IsCompleted), ref _IsCompleted, value); }
        }
        private Project _Project;
        [Association("Project-Milestones")]
        public Project Project
        {
            get { return _Project; }
            set { SetPropertyValue(nameof(Project), ref _Project, value); }
        }

        [Association("Milestone-Calendar")]
        public XPCollection<Calendar> Calendars
        {
            get { return GetCollection<Calendar>(nameof(Calendars)); }
        }


    }
}