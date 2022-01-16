using System;
using System.Runtime.InteropServices;

namespace BlazorApp.Services
{
    [Guid("B2E10AE2-1E5B-42DA-A57C-B8AF04F27182")]
    public class AlertService : ServiceBase, IAlertService
    {
        public event AlertServiceEventHandler OnAlert = null;

        /// <summary>
        /// Show custom alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowAlert(AlertType type, string message, int closeDelay)
        {
            ShowAlert(new AlertEventArgs(new Alert(type, message, closeDelay)));
        }

        /// <summary>
        /// Show custom alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowAlert(AlertType type, string message, bool autoClose)
        {
            ShowAlert(new AlertEventArgs(new Alert(type, message, autoClose)));
        }

        /// <summary>
        /// Show success alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowSuccessAlert(string message)
        {
            ShowAlert(new AlertEventArgs(new Alert(AlertType.Success, message)));
        }

        /// <summary>
        /// Show error alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowErrorAlert(string message)
        {
            ShowAlert(new AlertEventArgs(new Alert(AlertType.Danger, message)));
        }

        /// <summary>
        /// Show info alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowInfoAlert(string message)
        {
            ShowAlert(new AlertEventArgs(new Alert(AlertType.Info, message)));
        }
        
        /// <summary>
        /// Show warning alert on the razor pages
        /// </summary>
        /// <param name="message"></param>
        public void ShowWarningAlert(string message)
        {
            ShowAlert(new AlertEventArgs(new Alert(AlertType.Warning, message)));
        }

        private void ShowAlert(AlertEventArgs e)
        {
            if (OnAlert != null)
                OnAlert(e);
        }
    }
}
