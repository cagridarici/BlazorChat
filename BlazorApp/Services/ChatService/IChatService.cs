using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IChatService : IService
    {
        public event MessageEventHandler MessageReceived;
        public event EmptyEventHander StatesChanged;

        public event UserEventHandler UserConnected;
        public event UserEventHandler UserDisconnected;
        List<User> OnlineUsers { get; }

        Task SendMessage(MessageModel message);
        Task GetOnlineUsers();
    }
}
