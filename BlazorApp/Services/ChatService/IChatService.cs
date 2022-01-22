using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IChatService : IService
    {
        public event MessageEventHandler GetMessage;
        public event EmptyEventHander OnStatesChanged;

        List<User> OnlineUsers { get; }

        Task SendMessage(MessageModel message);
        Task GetOnlineUsers();
    }
}
