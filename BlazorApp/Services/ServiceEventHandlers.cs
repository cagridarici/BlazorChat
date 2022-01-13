using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public delegate void EmptyEventHander();
    public delegate void UserEventHandler(UserEventArgs e);
    public delegate void AlertServiceEventHandler(AlertEventArgs e);
    public delegate void HubConnectionEventHandler(string connectionId);
}
