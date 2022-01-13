﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class ChatService : ServiceBase, IChatService
    {
        public event EmptyEventHander OnStatesChanged = null;

        List<User> _ChatUsers = null;
        HubService _HubService = null;
        bool _IsRegisterHubMethods = false;

        public ChatService()
        {
            _ChatUsers = new List<User>();
            _HubService = ServiceContainer.Instance.GetServiceInstance(typeof(IHubService)) as HubService;
        }

        public void OnConnectedUser(UserEventArgs e)
        {
            _ChatUsers.Add(e.GetUser);
        }

        private void RegisterHubMethods()
        {
            if (_IsRegisterHubMethods)
                return;
            _HubService.RegisterCustomHubMethod<List<User>>(Commands.GET_CHAT_USERS, LoadChatUsers);
            _HubService.RegisterCustomHubMethod<User>(Commands.GET_ON_CONNECTED_USER, CommandGetOnConnectedUser);
        }

        public async Task GetChatUsers()
        {
            RegisterHubMethods();
            await _HubService.InvokeAsync(Commands.GET_CHAT_USERS);
        }

        public void LoadChatUsers(List<User> chatUsers)
        {
            _ChatUsers = chatUsers;
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        public void CommandGetOnConnectedUser(User user)
        {
            _ChatUsers.Add(user);
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        public List<User> ChatUsers
        {
            get
            {
                return _ChatUsers;
            }
        }
    }
}