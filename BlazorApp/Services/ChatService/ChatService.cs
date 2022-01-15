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
            _HubService.RegisterCustomHubMethod<MessageModel>(Commands.GET_MESSAGE, GetMessage);
            _HubService.RegisterCustomHubMethod<User>(Commands.SEND_CLIENT_TO_NEW_USER, GetChangedUser);
        }

        public void GetChangedUser(User user)
        {
            var userItem = ChatUsers.Where(x => x.ConnectionId == user.ConnectionId).FirstOrDefault();
            if (userItem == null)
                return;
            userItem.UserStatus = user.UserStatus;
            if (OnStatesChanged != null)
            {
                OnStatesChanged();
            }
        }

        public async Task GetChatUsers()
        {
            RegisterHubMethods();
            await _HubService.InvokeAsync(Commands.GET_CHAT_USERS);
        }

        public void GetMessage(MessageModel message)
        {
            if (OnGetMessageEventHandler != null)
            {
                OnGetMessageEventHandler(message);
            }
        }

        public async Task SendMessage(MessageModel model)
        {
            RegisterHubMethods();
            await _HubService.InvokeAsync(Commands.SEND_MESSAGE, model);
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
