using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class ChatService : ServiceBase, IChatService
    {
        public event EmptyEventHander OnStatesChanged = null;
        public event MessageEventHandler OnGetMessageEventHandler = null;

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
            _HubService.SubscribeCustomHubMethod<User>(ClientCommands.RECEIVE_LOGGED_USER, ReceiveLoggedUser);
            _HubService.SubscribeCustomHubMethod<MessageModel>(ClientCommands.RECEIVE_MESSAGE, ReceiveMessage);
            _HubService.SubscribeCustomHubMethod<User>(ClientCommands.RECEIVE_CHANGED_USER_STATUS, ReceiveChangedUser);
        }

        public async Task GetOnlineUsers()
        {
            await _HubService.InvokeAsync(HubCommands.SEND_ONLINE_USERS);
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
            if (OnGetMessageEventHandler != null)
            {
                OnGetMessageEventHandler(message);
            }
        }
 
        private void ReceiveOnlineUsers(List<User> onlineUsers)
        {
            _OnlineUsers = onlineUsers;
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        private void ReceiveLoggedUser(User user)
        {
            _OnlineUsers.Add(user);
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        private void ReceiveChangedUser(User user)
        {
            var userItem = _OnlineUsers.Where(x => x.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (userItem == null)
                return;
            userItem.UserStatus = user.UserStatus;
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        #endregion

        public List<User> OnlineUsers
        {
            get
            {
                return _OnlineUsers;
            }
        }
    }
}
