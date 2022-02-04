using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class User
    {
        string _ConnectionId = string.Empty;
        string _Name = string.Empty;
        string _Surname = string.Empty;
        string _ImageUrl = string.Empty;
        DateTime _ConnectedDate = DateTime.MinValue;
        bool _IsConnect = false;
        UserStatus _Status = UserStatus.Offline;

        public string ConnectionId
        {
            get { return _ConnectionId; }
            set { _ConnectionId = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }

        public string FullName
        {
            get { return _Name + " " + _Surname; }
        }

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        public bool IsConnect
        {
            get { return _IsConnect; }
            set { _IsConnect = value; }
        }

        public DateTime ConnectedDate
        {
            get { return _ConnectedDate; }
            set { _ConnectedDate = value; }
        }

        public UserStatus UserStatus
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }

    public enum UserStatus
    {
        None,
        Offline,
        Online,
        Away
    }
}
