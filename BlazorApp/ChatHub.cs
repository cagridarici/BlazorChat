﻿using BlazorApp.Services;
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

        List<User> _OnlineUsers = null;

        public ChatHub()
        {
            _OnlineUsers = new List<User>();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [HubMethodName(HubCommands.CONNECT_CLIENT)]
        public void ConnectClient(User user)
        {
            user.ConnectionId = this.Context.ConnectionId;
            user.IsConnect = true;
            _OnlineUsers.Add(user);
            Clients.Caller.SendAsync(ClientCommands.GET_CONNECTION_ID, this.Context.ConnectionId);
            SendConnectedUser(user);
        }

        [HubMethodName(HubCommands.SEND_ONLINE_USERS)]
        public void SendOnlineUsers()
        {
            Clients.Caller.SendAsync(ClientCommands.GET_ONLINE_USERS, _OnlineUsers);
        }

        [HubMethodName(HubCommands.SEND_MESSAGE)]
        public void SendMessage(MessageModel model)
        {
            Clients.All.SendAsync(ClientCommands.GET_MESSAGE, model);
        }

        [HubMethodName(HubCommands.CHANGE_USER_STATUS)]
        public void ChangeUserProperties(User user)
        {
            var changeItem = _OnlineUsers.Where(x => x.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (changeItem == null)
                return;
            changeItem.UserStatus = user.UserStatus;
            Clients.All.SendAsync(ClientCommands.GET_CHANGED_USER_STATUS, changeItem);
        }

        private void SendConnectedUser(User user)
        {
            Clients.All.SendAsync(ClientCommands.GET_LOGGED_USER, user);
        }
    }
}
