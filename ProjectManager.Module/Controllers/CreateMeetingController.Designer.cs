using DevExpress.ExpressApp.Actions;

namespace ProjectManager.Module.Controllers
{
    partial class CreateMeetingController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public PopupWindowShowAction ActionCreateMeeting { get; private set; }

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
           this.ActionCreateMeeting = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // ActionCreateMeeting
            // 
            this.ActionCreateMeeting.AcceptButtonCaption = null;
            this.ActionCreateMeeting.CancelButtonCaption = null;
            this.ActionCreateMeeting.Caption = "Create Meeting";
            this.ActionCreateMeeting.ConfirmationMessage = "Are you sure you want to create a meeting?";
            this.ActionCreateMeeting.Id = "ActionCreateMeeting";
            this.ActionCreateMeeting.ToolTip = "Create a meeting in the company";
            this.ActionCreateMeeting.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ActionCreateMeeting_CustomizePopupWindowParams);
            // 
            // CreateMeetingFromCompanyController
            // 
            this.TargetObjectType = typeof(ProjectManager.Module.BusinessObjects.Team);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.TargetViewId = "Team_DetailView";
            this.Actions.Add(this.ActionCreateMeeting);



        }


        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        

        #endregion
    }
}
