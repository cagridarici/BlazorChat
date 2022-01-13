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

        static List<User> OnlineUsers = new List<User>();

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [HubMethodName(Commands.CONNECT_CLIENT)]
        public void ConnectClient(User user)
        {
            user.ConnectionId = this.Context.ConnectionId;
            OnlineUsers.Add(user);
            Clients.Caller.SendAsync(Commands.GET_CONNECTION_ID, this.Context.ConnectionId);
            SendOnConnectedUser(user);
        }

        [HubMethodName(Commands.GET_CHAT_USERS)]
        public void SendChatUsers()
        {
            Clients.Caller.SendAsync(Commands.GET_CHAT_USERS, OnlineUsers);
        }

        public void SendOnConnectedUser(User user)
        {
            Clients.All.SendAsync(Commands.GET_ON_CONNECTED_USER, user);
        }

        [HubMethodName(Commands.SEND_MESSAGE)]
        public void SendMessage(MessageModel model)
        {
            Clients.All.SendAsync(Commands.GET_MESSAGE, model);
        }
    }
}
