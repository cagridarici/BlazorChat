using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IChatService : IService
    {
        //public event UserEventHandler OnConnectedUser;
        //public event UserEventHandler OnDisconnectedUser;

        public event EmptyEventHander OnStatesChanged;

        List<User> ChatUsers { get; }
        Task GetChatUsers();
        void LoadChatUsers(List<User> chatUsers);
    }
}
