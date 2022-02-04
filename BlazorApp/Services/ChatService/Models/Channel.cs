using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class Channel
    {
        Guid _ChannelId = Guid.Empty;
        string _Name = string.Empty;
        List<User> _ConnectedUsers = null;

        public Channel()
        {
            _ChannelId = Guid.NewGuid();
            _ConnectedUsers = new List<User>();
        }

        public void Join(User user)
        {
            _ConnectedUsers.Add(user);
            ShowAlert(string.Format("{0} isimli kanala giriş yaptınız...", _Name));
        }

        public void Quit(User user)
        {
            if (_ConnectedUsers.Contains(user))
            {
                _ConnectedUsers.Remove(user);
                ShowAlert(string.Format("{0} isimli kanaldan çıkış yaptınız...", _Name));
            }
        }

        private void ShowAlert(string alertMessage)
        {

        }





    }
}
