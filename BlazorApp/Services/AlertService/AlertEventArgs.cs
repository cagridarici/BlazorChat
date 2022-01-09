using System;

namespace BlazorApp.Services
{
    public class AlertEventArgs : EventArgs
    {
        Alert _Alert = null;

        public AlertEventArgs(Alert alert)
        {
            _Alert = alert;
        }

        public Alert GetAlert { get { return _Alert; } }
    }
}
