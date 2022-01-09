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

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [HubMethodName(Commands.ConnectClient)]
        public void ConnectClient(User user)
        {
            Clients.All.SendAsync("OnConnectedHub", this.Context.ConnectionId);
        }
    }
}
