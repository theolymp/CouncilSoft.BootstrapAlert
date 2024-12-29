#region usings

using System;
using System.Collections.Generic;
using System.Web.Mvc;

#endregion

namespace CouncilSoft.BootstrapAlert
{
    /// <summary>
    ///     Static utility class for managing client side status messages.
    /// </summary>
    public static class AlertManager
    {
        /// <summary>
        ///     Sets the alert for the mvc view to render. Rendered by Html.RenderAlertMessages().
        /// </summary>
        /// <param name="controllerBase">The MVC controller from which this call is being made.</param>
        /// <param name="alert">The populated alert to show to the user.</param>
        /// <exception cref="ArgumentNullException">If either argument is null.</exception>
        public static void AppendAlert(ControllerBase controllerBase, AlertDetail alert)
        {
            if (controllerBase == null)
                throw new ArgumentNullException(nameof(controllerBase));
            if (alert == null)
                throw new ArgumentNullException(nameof(alert));

            Queue<AlertDetail> queue;

            // Get the queue
            if (alert.EnableCrossView)
                queue = controllerBase.TempData["CouncilSoft.BootstrapAlerts"] as Queue<AlertDetail>;
            else
                queue = controllerBase.ViewData["CouncilSoft.BootstrapAlerts"] as Queue<AlertDetail>;

            // Or create the queue
            if (queue == null)
                queue = new Queue<AlertDetail>();

            // Enqueue the item
            queue.Enqueue(alert);

            // Persist the updated queue.
            if (alert.EnableCrossView)
                controllerBase.TempData["CouncilSoft.BootstrapAlerts"] = queue;
            else
                controllerBase.ViewData["CouncilSoft.BootstrapAlerts"] = queue;
        }
    }
}