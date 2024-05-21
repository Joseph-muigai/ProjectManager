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
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ProjectManager.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ShowMyNotificationsController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        private NotificationCollectionCollectionService _notificationService;
        public ShowMyNotificationsController(Session session) : base()
        {
            InitializeComponent();
            _notificationService = new NotificationCollectionCollectionService(Application.CreateObjectSpace(typeof(NotificationCollection)));
            SimpleAction showNotificationsAction = new SimpleAction(this, "ShowNotificationsAction", PredefinedCategory.View)
            {
                Caption = "Show Notifications",
                ImageName = "Action_Notifications"
            };
            showNotificationsAction.Execute += ShowNotificationsAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void ShowNotificationsAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            

        }
    }
}
