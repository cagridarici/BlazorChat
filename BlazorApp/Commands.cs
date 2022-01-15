namespace BlazorApp
{
    public class Commands
    {
        #region Login

        public const string CONNECT_SIGNALR_HUB = "CommandConnectSignalR";
        public const string GET_CONNECTION_ID = "CommandGetConnectionId";
        public const string CONNECT_CLIENT = "CommandConnectClient";
        public const string DISCONNECT_CLIENT = "CommandDisconnectClient";
        public const string CHANGE_USER_PROPERTIES = "CommandChangeUserProperties";
        public const string SEND_CLIENT_TO_NEW_USER = "CommandSendClientToNewUser";

        #endregion

        #region Chat

        public const string GET_CHAT_USERS = "CommandGetChatUsers";
        public const string GET_ON_CONNECTED_USER = "CommandGetOnConnectedUser";

        public const string GET_MESSAGE = "CommandGetMessage";
        public const string SEND_MESSAGE = "CommandSendMessage";

        #endregion


    }
}
