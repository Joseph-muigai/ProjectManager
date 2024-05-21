namespace ProjectManager.Module.Controllers
{
    partial class AddTaskToCalendar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            //
            // AddTaskToCalendar
            //   

            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewId = "Task_DetailView";
            this.AddTaskToCalendarAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.AddTaskToCalendarAction.Caption = "Add Task To Calendar";
            this.AddTaskToCalendarAction.ConfirmationMessage = null;
            this.AddTaskToCalendarAction.Id = "AddTaskToCalendarAction";
            this.AddTaskToCalendarAction.ImageName = "Action_Add";
            this.AddTaskToCalendarAction.ToolTip = null;
            this.AddTaskToCalendarAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AddTaskToCalendarAction_Execute);

            this.components = new System.ComponentModel.Container();
            this.TargetObjectType = typeof(ProjectManager.Module.BusinessObjects.Task);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.Activated += new System.EventHandler(this.AddTaskToCalendar_Activated);
            this.Actions.Add(this.AddTaskToCalendarAction);
        }

        private DevExpress.ExpressApp.Actions.SimpleAction AddTaskToCalendarAction;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion
    }
}
