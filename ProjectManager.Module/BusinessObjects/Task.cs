using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base.General;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Xpo;


namespace ProjectManager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Appearance("TaskCompleted", AppearanceItemType = "ViewItem", TargetItems = "*",
         Context = "ListView", Criteria = "Status = 'Completed'", FontColor = "Green", FontStyle = DevExpress.Drawing.DXFontStyle.Strikeout)]
    [Appearance("TaskInProgress", AppearanceItemType = "ViewItem", TargetItems = "*",
                Context = "ListView", Criteria = "Status = 'InProgress'", FontColor = "Blue")]

    [Appearance("TaskDefered", AppearanceItemType = "ViewItem", TargetItems = "*",
                       Context = "ListView", Criteria = "Status = 'Deferred'", FontColor = "Red")]

    [RuleCriteria("EndDateAfterStartDate", DefaultContexts.Save, "EndDate >= StartDate", "End date must be greater than or equal to start date")]
    // startdate not in the past
    [RuleCriteria("StartDateNotInPast", DefaultContexts.Save, "StartDate >= Today()", "Start date must be greater than or equal to today")]
    // enddate not in the past
    [RuleCriteria("EndDateNotInPast", DefaultContexts.Save, "EndDate >= Today()", "End date must be greater than or equal to today")]
    //start date  cannot be earlier than the start date of the assigned project
    [RuleCriteria("StartDateNotBeforeProjectStartDate", DefaultContexts.Save, "StartDate >= Project.StartDate", "Start date must be greater than or equal to project start date")]
    //end date cannot be later than the end date of the assigned project
    [RuleCriteria("EndDateNotAfterProjectEndDate", DefaultContexts.Save, "EndDate <= Project.DueDate", "End date must be less than or equal to project end date")]

 
    public class Task : BaseObject
    {
        public Task(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Send notification if task is assigned to a user
            AssignedBy = SecuritySystem.CurrentUserName;

            //---------------------------------------------------
            //add the task to calendar if the task is assigned
            if (AssignedTo != null)
            {
                Calendar calendarEvent = new Calendar(Session)
                {
                    Subject = TaskName,
                    Description = TaskDescription,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    //AssignedTo = AssignedTo
                };
            
                calendarEvent.Session.CommitTransaction();
            }
            //---------------------------------------------------

        }
        protected override void OnSaved()
        {
            base.OnSaved();

            // create a new notification for the assigned user if the task is assigned 
            if (AssignedTo != null && AssignedBy != null)
            {
                NotificationCollection notification = new NotificationCollection(Session)
                {
                    NotificationText = $"You have been assigned a new task: {TaskName}",
                    NotificationDate = DateTime.Now,
                    AssignedTo = AssignedTo
                };
               
                notification.Session.CommitTransaction();
            }
            //// check if task is started and set the project status to in progress
            //if (Project.Tasks.Any(t => t.Status == Status.InProgress))
            //{
            //    Project.Status = Status.InProgress;
            //    Project.Save();
            //    Project.Session.CommitTransaction();
            //}
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == nameof(Status) && newValue != null && oldValue != null)
            {
                //set the end date to the current date if the task is completed
                if ((Status)newValue == Status.Completed)
                {
                    EndDate = DateTime.Now;
                }

            }
            // create a new notification for the assigned user if the task is completed
            if (propertyName == nameof(Status) && newValue != null && oldValue != null)
            {
                if ((Status)newValue == Status.Completed)
                {
                    NotificationCollection notification = new NotificationCollection(Session)
                    {
                        NotificationText = $"The task: {TaskName} has been completed",
                        NotificationDate = DateTime.Now,
                        AssignedTo = AssignedTo
                    };
                    notification.Save();
                    notification.Session.CommitTransaction();
                }
            }
            //adjust the dates in the calendar if the task is updated
            if (propertyName == nameof(StartDate) || propertyName == nameof(EndDate))
            {
                Calendar calendarEvent = Session.FindObject<Calendar>(CriteriaOperator.Parse($"Subject = '{TaskName}'"));
                if (calendarEvent != null)
                {
                    calendarEvent.StartDate = StartDate;
                    calendarEvent.EndDate = EndDate;
                    calendarEvent.Save();
                    calendarEvent.Session.CommitTransaction();
                }
            }
            // check if task is started and set the project status to in progress
            if (propertyName == nameof(Status) && newValue != null && oldValue != null)
            {
                if ((Status)newValue == Status.InProgress)
                {
                    Project.Status = Status.InProgress;
                    Project.Save();
                    Project.Session.CommitTransaction();
                }
            }

        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
            // create a new notification for the assigned user if the task is deleted
            if (AssignedTo != null)
            {
                NotificationCollection notification = new NotificationCollection(Session)
                {
                    NotificationText = $"The task: {TaskName} has been deleted",
                    NotificationDate = DateTime.Now,
                    AssignedTo = AssignedTo
                };

                notification.Session.CommitTransaction();
            }
            //remove the task from the calendar if the task is deleted
            if (AssignedTo != null)
            {
                Calendar calendarEvent = Session.FindObject<Calendar>(CriteriaOperator.Parse($"Subject = '{TaskName}'"));
                if (calendarEvent != null)
                {
                    calendarEvent.Delete();
                }
            }
        }



        private string _TaskName;
        public string TaskName
        {
            get { return _TaskName; }
            set { SetPropertyValue(nameof(TaskName), ref _TaskName, value); }
        }
        private string _TaskDescription;
        [Size(SizeAttribute.Unlimited)]
        public string TaskDescription
        {
            get { return _TaskDescription; }
            set { SetPropertyValue(nameof(TaskDescription), ref _TaskDescription, value); }
        }
        private Project _Project;
        [DevExpress.Xpo.Association("Project-Tasks")]
        public Project Project
        {
            get { return _Project; }
            set { SetPropertyValue(nameof(Project), ref _Project, value); }
        }
        private DateTime _StartDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { SetPropertyValue(nameof(StartDate), ref _StartDate, value); }
        }
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { SetPropertyValue(nameof(EndDate), ref _EndDate, value); }
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
        private ApplicationUser _AssignedTo;
        [DevExpress.Xpo.Association("Tasks-ApplicationUser")]
        public ApplicationUser AssignedTo
        {
            get { return _AssignedTo; }
            set { SetPropertyValue(nameof(AssignedTo), ref _AssignedTo, value); }
        }
        [DevExpress.Xpo.Association("Task-Comments")]
        public XPCollection<Comment> Comments
        {
            get { return GetCollection<Comment>(nameof(Comments)); }
        }
        private string _AssignedFrom;
        [VisibleInDetailView(false)]
        public string AssignedBy
        {
            //currently logged in user
            get
            {
                return _AssignedFrom;
            }
            set { SetPropertyValue(nameof(AssignedBy), ref _AssignedFrom, value); }
        }
        [DevExpress.Xpo.Association("Task-FeedBacks")]
        public XPCollection<FeedBack> FeedBacks
        {
            get { return GetCollection<FeedBack>(nameof(FeedBacks)); }
        }
    }
    
    public enum Status {
        [ImageName("State_Task_NotStarted")]
        NotStarted = 0,
        [ImageName("State_Task_InProgress")]
        InProgress = 1,
        [ImageName("State_Task_Completed")]
        Completed = 2,
        [ImageName("State_Task_Deferred")]
        Deferred = 3,
        [ImageName("State_Task_WaitingOnSomeoneElse")]
        WaitingOnSomeoneElse = 4
    }

    public enum Priority
    {
        [ImageName("State_Priority_Low")]
        Low = 0,
        [ImageName("State_Priority_Normal")]
        Normal = 1,
        [ImageName("State_Priority_High")]
        High = 2
    }
}