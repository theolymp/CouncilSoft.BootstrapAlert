#region usings

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

#endregion

namespace CouncilSoft.BootstrapAlert.Extensions
{
    /// <summary>
    ///     Extension class for HtmlHelper.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        ///     Processes a single queue and generates appropriate jQuery to show the alerts.
        /// </summary>
        /// <param name="queue">The queue to process, or null.</param>
        public static string ProcessQueue(Queue<AlertDetail> queue)
        {
            var result = new StringBuilder();

            if (queue != null)
                while (queue.Count > 0)
                {
                    var alert = queue.Dequeue();

                    result.AppendLine(string.Format(
                        @"<script>$().ready(function () {{ addAlertToMessageArea({0}, '{1}',{2},{3}); }});</script>",
                        (int)alert.Severity, alert.AlertMessage, alert.AutoDismissTime.TotalMilliseconds,
                        alert.ShowDismissButton ? "1" : "0"));
                }

            return result.ToString();
        }

        /// <summary>
        ///     Renders an alert message container, a div with an id of "messageArea".
        /// </summary>
        /// <param name="instance">The instance of HtmlHelper.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string RenderAlertMessageContainer(this HtmlHelper instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            return "<div id=\"messageArea\"></div>";
        }

        /// <summary>
        ///     Renders all available alerts from TempData and ViewData, and then clears the queue.
        /// </summary>
        /// <param name="instance">The instance of HtmlHelper.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Various script and HTML to render messages to the UI.</returns>
        public static string RenderAlertMessagesFromQueue(this HtmlHelper instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var result = new StringBuilder();
            Queue<AlertDetail> queue;

            // Handle cross-view messages (via TempData)
            queue = instance.ViewContext.Controller.TempData["CouncilSoft.BootstrapAlerts"] as Queue<AlertDetail>;
            result.AppendLine(ProcessQueue(queue));
            instance.ViewContext.Controller.TempData["CouncilSoft.BootstrapAlerts"] = new Queue<AlertDetail>();

            // Handle same-view messages (via ViewData)
            queue = instance.ViewContext.Controller.ViewData["CouncilSoft.BootstrapAlerts"] as Queue<AlertDetail>;
            result.AppendLine(ProcessQueue(queue));
            instance.ViewContext.Controller.ViewData["CouncilSoft.BootstrapAlerts"] = new Queue<AlertDetail>();

            return result.ToString();
        }
    }
}