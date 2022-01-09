using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IAlertService : IService
    {
        event AlertServiceEventHandler OnAlert;
        void ShowAlert(AlertType type, string message, bool autoClose, int closeDelay);
        void ShowSuccessAlert(string message);
        void ShowErrorAlert(string message);
        void ShowInfoAlert(string message);
        void ShowWarningAlert(string message);
    }
}
