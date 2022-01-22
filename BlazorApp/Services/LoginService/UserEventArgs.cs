using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class UserEventArgs : EventArgs
    {
        User _User = null;

        public UserEventArgs(User user)
        {
            _User = user;
        }

        public User User
        {
            get { return _User; }
        }
    }
}
