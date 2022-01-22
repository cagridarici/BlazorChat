using BlazorApp.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class ChatHub : Hub
    {
        public const string HUB_URL = "/ChatHub";

        static List<User> _OnlineUsers = null;

        static ChatHub()
        {
            _OnlineUsers = new List<User>();
        }

        [HubMethodName(HubCommands.CONNECT_CLIENT)]
        public void ConnectClient(User user)
        {
            user.ConnectionId = this.Context.ConnectionId;
            user.IsConnect = true;
            _OnlineUsers.Add(user);
            Clients.Caller.SendAsync(ClientCommands.RECEIVE_CONNECTION_ID, this.Context.ConnectionId);
            SendConnectedUser(user);
        }

        [HubMethodName(HubCommands.DISCONNECT_CLIENT)]
        public void DisconnectClient(User user)
        {
            User deletedUser = _OnlineUsers.Where(i => i.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (deletedUser != null)
            {
                _OnlineUsers.Remove(deletedUser);
                SendDisconnectedUser(user);
            }
        }

        [HubMethodName(HubCommands.GET_ONLINE_USERS)]
        public void GetOnlineUsers()
        {
            Clients.Caller.SendAsync(ClientCommands.RECEIVE_ONLINE_USERS, _OnlineUsers);
        }

        [HubMethodName(HubCommands.SEND_MESSAGE)]
        public void SendMessage(MessageModel model)
        {
            Clients.All.SendAsync(ClientCommands.RECEIVE_MESSAGE, model);
        }

        [HubMethodName(HubCommands.CHANGE_USER_STATUS)]
        public void ChangeUserProperties(User user)
        {
            var changeItem = _OnlineUsers.Where(x => x.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (changeItem == null)
                return;
            changeItem.UserStatus = user.UserStatus;
            Clients.All.SendAsync(ClientCommands.RECEIVE_CHANGED_USER_STATUS, changeItem);
        }

        private void SendConnectedUser(User user)
        {
            Clients.All.SendAsync(ClientCommands.RECEIVE_CONNECTED_USER, user);
        }

        private void SendDisconnectedUser(User user)
        {
            Clients.All.SendAsync(ClientCommands.RECEIVE_DISCONNECTED_USER, user);
        }
    }
}
