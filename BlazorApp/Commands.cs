using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Commands
    {
        public const string ConnectSignalR = "CommandConnectSignalR";
        public const string GetConnectionId = "CommandGetConnectionId";
        public const string ConnectClient = "CommandConnectClient";
        public const string DisconnectClient = "CommandDisconnectClient";
    }
}
