using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class HubResponse : IHubResponse
    {
        bool _IsSuccess = false;
        string _Message = null;
        ArrayList _Parameters = null;

        public HubResponse()
        {

        }

        public void AddParameter(object parameter)
        {
            _Parameters.Add(parameter);
        }

        public bool IsSuccess
        {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public ArrayList Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }
    }
}
