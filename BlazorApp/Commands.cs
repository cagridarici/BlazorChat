using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class ClientCommands
    {
        public const string RECEIVE_CONNECTION_ID = "COMMAND_RECEIVE_CONNECTION_ID";
        public const string RECEIVE_ONLINE_USERS = "COMMAND_RECEIVE_ONLINE_USERS";
        public const string RECEIVE_CONNECTED_USER = "COMMAND_RECEIVE_CONNECTED_USER";
        public const string RECEIVE_DISCONNECTED_USER = "COMMAND_RECEIVE_DISCONNECTED_USER";
        public const string RECEIVE_CHANGED_USER_STATUS = "COMMAND_RECEIVE_CHANGED_USER_STATUS";
        public const string RECEIVE_MESSAGE = "COMMAND_RECEIVE_MESSAGE";
    }

    public class HubCommands
    {
        /// <summary>
        /// Connect the client to signal r hub.
        /// </summary>
        public const string CONNECT_HUB = "COMMAND_CONNECT_HUB";
        public const string CONNECT_CLIENT = "COMMAND_CONNECT_CLIENT";
        public const string SEND_MESSAGE = "COMMAND_SEND_MESSAGE";
        public const string GET_ONLINE_USERS = "COMMAND_GET_ONLINE_USERS";
        public const string DISCONNECT_CLIENT = "COMMAND_DISCONNECT_CLIENT";
        public const string CHANGE_USER_STATUS = "COMMAND_CHANGE_USER_STATUS";
    }
}
