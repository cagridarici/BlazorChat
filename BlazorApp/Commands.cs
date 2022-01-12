using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Commands
    {
        public const string CONNECT_SIGNALR_HUB = "CommandConnectSignalR";
        public const string GET_CONNECTION_ID = "CommandGetConnectionId";
        public const string CONNECT_CLIENT = "CommandConnectClient";
        public const string DISCONNECT_CLIENT = "CommandDisconnectClient";
        public const string SEND_HUB_RESULT = "CommandSendHubResult";

    }
}
