namespace BlazorApp
{
    public class Commands
    {
        #region Login

        public const string CONNECT_SIGNALR_HUB = "CommandConnectSignalR";
        public const string GET_CONNECTION_ID = "CommandGetConnectionId";
        public const string CONNECT_CLIENT = "CommandConnectClient";
        public const string DISCONNECT_CLIENT = "CommandDisconnectClient";

        #endregion

        #region Chat

        public const string GET_CHAT_USERS = "CommandGetChatUsers";
        public const string GET_ON_CONNECTED_USER = "CommandGetOnConnectedUser";


        #endregion


    }
}
