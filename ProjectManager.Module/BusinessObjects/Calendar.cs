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
    [Browsable(false)]
    [ReadOnly(true)]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Calendar : Event
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Calendar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Task Task = new Task(Session);
            EndDate = Task.EndDate;
            StartDate = Task.StartDate;
            //StartDate = Session.GetObjectByKey<Task>(Task.StartDate);
            //EndDate = Session.GetObjectByKey<Task>(Task.EndDate);


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

        //create a calendar for each project and milestone as an event in the calendar
        private Project _Project;
        [Association("Project-Calendar")]
        public Project Project
        {
            get { return _Project; }
            set { SetPropertyValue(nameof(Project), ref _Project, value); }
        }

        private Milestone _Milestone;
        [Association("Milestone-Calendar")]
        public Milestone Milestone
        {
            get { return _Milestone; }
            set { SetPropertyValue(nameof(Milestone), ref _Milestone, value); }
        }


        private DateTime _EndDate;
        public DateTime EndDate {
            get { return _EndDate; }
            set { SetPropertyValue(nameof(EndDate), ref _EndDate, value); }
        }
      private DateTime _StartDate;  
        public DateTime StartDate {
            get { return _StartDate; }
            set { SetPropertyValue(nameof(StartDate), ref _StartDate, value); }
        }


       
        private bool _AllDay;
        public bool AllDay
        {
            //set the event to be an all day event
            get { return true; }

        }


    }
}