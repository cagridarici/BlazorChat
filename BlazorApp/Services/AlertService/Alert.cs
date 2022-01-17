using System;

namespace BlazorApp.Services
{
    public class Alert
    {
        Guid _Id = Guid.Empty;
        AlertType _Type = AlertType.None;
        string _Message = string.Empty;
        bool _AutoClose = true;
        int _CloseDelay = 5000;
        bool _FadeEffect = false;

        public Alert(AlertType type, string message)
        {
            _Type = type;
            _Message = message;
            _Id = Guid.NewGuid();
        }

        public Alert(AlertType type, string message, int closeDelay)
        {
            _Type = type;
            _Message = message;
            _Id = Guid.NewGuid();
            _CloseDelay = closeDelay;
        }

        public Alert(AlertType type, string message, bool autoClose)
        {
            _Type = type;
            _Message = message;
            _Id = Guid.NewGuid();
            _AutoClose = autoClose;
        }

        public Guid Id
        {
            get { return _Id; }
        }

        public AlertType Type
        {
            get { return _Type; }
        }

        public string Message
        {
            get { return _Message; }
        }

        public bool AutoClose
        {
            get { return _AutoClose; }
        }

        public int CloseDelay
        {
            get { return _CloseDelay; }
        }

        public string GetClassName
        {
            get { return _Type.ToString().ToLower(); }
        }

        public bool FadeEffect
        {
            get { return _FadeEffect; }
            set { _FadeEffect = value; }
        }

        public string GetTypeText()
        {
            switch (Type)
            {
                case AlertType.None:
                    return string.Empty;
                case AlertType.Success:
                    return "Başarılı";
                case AlertType.Danger:
                    return "Dikkat";
                case AlertType.Info:
                    return "Bilgi";
                case AlertType.Warning:
                    return "Uyarı";
                default:
                    return string.Empty;
            }
        }
    }

    public enum AlertType
    {
        None,
        Success,
        Danger,
        Info,
        Warning
    }
}
