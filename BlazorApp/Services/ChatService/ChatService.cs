﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class ChatService : ServiceBase, IChatService
    {
        public event MessageEventHandler MessageReceived = null;
        public event EmptyEventHander StatesChanged = null;

        public event UserEventHandler UserDisconnected = null;
        public event UserEventHandler UserConnected = null;

        List<User> _OnlineUsers = null;
        IHubService _HubService = null;

        public ChatService()
        {
            _OnlineUsers = new List<User>();
            _HubService = ServiceContainer.Instance.GetServiceInstance(typeof(IHubService)) as IHubService;
            SubscribeHubMethods();
        }

        protected override void SubscribeHubMethods()
        {
            if (!_HubService.IsConnected)
                return;
            _HubService.SubscribeCustomHubMethod<List<User>>(ClientCommands.RECEIVE_ONLINE_USERS, ReceiveOnlineUsers);
            _HubService.SubscribeCustomHubMethod<User>(ClientCommands.RECEIVE_CONNECTED_USER, ReceiveConnectedUser);
            _HubService.SubscribeCustomHubMethod<User>(ClientCommands.RECEIVE_DISCONNECTED_USER, ReceiveDisconnectedUser);
            _HubService.SubscribeCustomHubMethod<MessageModel>(ClientCommands.RECEIVE_MESSAGE, ReceiveMessage);
            _HubService.SubscribeCustomHubMethod<User>(ClientCommands.RECEIVE_CHANGED_USER_STATUS, ReceiveChangedUser);
        }

        public async Task GetOnlineUsers()
        {
            await _HubService.InvokeAsync(HubCommands.GET_ONLINE_USERS);
        }

        public async Task SendMessage(MessageModel model)
        {
            await _HubService.InvokeAsync(HubCommands.SEND_MESSAGE, model);
        }

        #region Receive Methods

        /// <summary>
        /// The values ​​of these methods are loaded by Signal R.
        /// </summary>
        private void ReceiveMessage(MessageModel message)
        {
            if (MessageReceived != null)
                MessageReceived(message);
        }

        private void ReceiveOnlineUsers(List<User> onlineUsers)
        {
            _OnlineUsers = onlineUsers;
            ChangeStates();
        }

        private void ReceiveConnectedUser(User user)
        {
            _OnlineUsers.Add(user);
            OnUserConnected(new UserEventArgs(user));
            ChangeStates();
        }

        private void ReceiveDisconnectedUser(User user)
        {
            User discUser = _OnlineUsers.Where(i => i.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (discUser != null)
            {
                _OnlineUsers.Remove(discUser);
                OnUserDisconnected(new UserEventArgs(discUser));
                ChangeStates();
            }
        }

        private void ReceiveChangedUser(User user)
        {
           
        }

        #endregion

        private void ChangeStates()
        {
            if (StatesChanged != null)
                StatesChanged();
        }

        private void OnUserConnected(UserEventArgs e)
        {
            if (UserConnected != null)
                UserConnected(e);
        }

        private void OnUserDisconnected(UserEventArgs e)
        {
            if (UserDisconnected != null)
                UserDisconnected(e);
        }

        public List<User> OnlineUsers
        {
            get
            {
                return _OnlineUsers;
            }
        }

    }
}
