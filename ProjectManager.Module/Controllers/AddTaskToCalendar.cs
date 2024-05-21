using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AddTaskToCalendar : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public AddTaskToCalendar()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(BusinessObjects.Task);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.ObjectSpace.Committed += ObjectSpace_Committed;
        }
        protected override void OnViewControlsCreated()
        {
            View.ObjectSpace.Committed -= ObjectSpace_Committed;
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        private void ObjectSpace_Committed(object sender, EventArgs e)
        {BusinessObjects.Task savedObject = View.CurrentObject as BusinessObjects.Task;
            if (savedObject != null)
            {
                BusinessObjects.Calendar calendarEvent = ObjectSpace.CreateObject<BusinessObjects.Calendar>();
                calendarEvent.Subject = savedObject.TaskName;
                calendarEvent.Description = savedObject.TaskDescription;
                calendarEvent.StartDate = savedObject.StartDate;
                calendarEvent.EndDate = savedObject.EndDate;


            }
        }

        void AddTaskToCalendarAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BusinessObjects.Task task = View.CurrentObject as BusinessObjects.Task;
            if (task != null)
            {
                //check if the task is already in the calendar
                BusinessObjects.Calendar calendarEvent = ObjectSpace.FindObject<BusinessObjects.Calendar>(new BinaryOperator("Subject", task.TaskName));
                if (calendarEvent != null)
                {
                    // toasts message to the user
                    Application.ShowViewStrategy.ShowMessage("Task already added to calendar", InformationType.Warning);
                    return;
                }
                // Create a new event
                //BusinessObjects.Calendar calendarEvent = ObjectSpace.CreateObject<BusinessObjects.Calendar>();
                calendarEvent.Subject = task.TaskName;
                calendarEvent.Description = task.TaskDescription;
                calendarEvent.StartDate = task.StartDate;
                calendarEvent.EndDate = task.EndDate;
                Event currentEvent = ObjectSpace.GetObject(calendarEvent);
                currentEvent.Subject = task.TaskName;
                currentEvent.Description = task.TaskDescription;
                currentEvent.StartOn = task.StartDate;
                currentEvent.EndOn = task.EndDate;
               
                if (currentEvent != null)
                {
                    try
                    {
                        ObjectSpace.CommitChanges();
                        // toasts message to the user
                        Application.ShowViewStrategy.ShowMessage("Task added to calendar", InformationType.Success);

                    }
                    catch (Exception ex)
                    {
                        ObjectSpace.Refresh();
                        throw new UserFriendlyException(ex.Message);
                    }
                    

                }
                //calendarEvent.Priority = task.Priority;
                //calendarEvent.Status = task.Status; 
                //calendarEvent.AllDay = true;
            }
        }
        private void AddTaskToCalendar_Activated(object sender, EventArgs e)
        {
            AddTaskToCalendarAction.Active.SetItemValue("Visible", true);
        }
    }


}
