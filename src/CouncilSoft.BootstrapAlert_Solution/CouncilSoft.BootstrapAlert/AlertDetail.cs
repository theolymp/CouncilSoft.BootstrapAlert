#region usings

using System;

#endregion

namespace CouncilSoft.BootstrapAlert
{
    /// <summary>
    ///     Data structure to hold the details of an alert.
    /// </summary>
    [Serializable]
    public class AlertDetail
    {
        /// <summary>
        ///     Creates a new instance of this type.
        /// </summary>
        public AlertDetail()
        {
            Severity = AlertSeverity.Info;
            AlertMessage = "No message set.";
            AutoDismissTime = new TimeSpan(0);
            ShowDismissButton = true;
            EnableCrossView = false;
        }

        /// <summary>
        ///     Gets or sets the actual message to display to the user.
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        ///     Gets or sets the time before message is automatically dismissed from the UI.
        ///     Default: 0, which disables the feature.
        /// </summary>
        public TimeSpan AutoDismissTime { get; set; }

        /// <summary>
        ///     Gets or sets whether this message should be made available across multiple
        ///     views (via TempData) or just the current view (ViewData). Default: ViewData.
        /// </summary>
        public bool EnableCrossView { get; set; }

        /// <summary>
        ///     Gets or sets the severity of the alert. Default: Info.
        /// </summary>
        public AlertSeverity Severity { get; set; }

        /// <summary>
        ///     Gets or sets whether to show the dismiss button to the right of the message.
        ///     Default: True.
        /// </summary>
        public bool ShowDismissButton { get; set; }
    }
}