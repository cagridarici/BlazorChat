using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class ClientCommands
    {
        public const string GET_CONNECTION_ID = "COMMAND_GET_CONNECTION_ID";
        public const string GET_ONLINE_USERS = "COMMAND_SEND_ONLINE_USERS";
        public const string GET_LOGGED_USER = "COMMAND_GET_LOGGED_USER";
        public const string GET_CHANGED_USER_STATUS = "COMMAND_GET_CHANGED_USER_STATUS";
        public const string GET_MESSAGE = "COMMAND_GET_MESSAGE";
    }

    public class HubCommands
    {
        /// <summary>
        /// Connect the client to signal r hub.
        /// </summary>
        public const string CONNECT_HUB = "COMMAND_CONNECT_HUB";
        public const string CONNECT_CLIENT = "COMMAND_CONNECT_CLIENT";
        public const string SEND_MESSAGE = "COMMAND_SEND_MESSAGE";
        public const string SEND_ONLINE_USERS = "COMMAND_SEND_ONLINE_USERS";
        public const string DISCONNECT_CLIENT = "COMMAND_DISCONNECT_CLIENT";
        public const string CHANGE_USER_STATUS = "COMMAND_CHANGE_USER_STATUS";
    }
}
