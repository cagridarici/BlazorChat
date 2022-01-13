using BlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    [Guid("D1641FE3-0AE6-41D1-BECA-9239FB4B01ED")]
    public class LoginService : ServiceBase, ILoginService
    {
        public event UserEventHandler OnConnectedUser = null;
        public event UserEventHandler OnDisconnectedUser = null;
        public event UserEventHandler OnUserChangedStatus = null; 

        HubService _HubService = null;

        public LoginService()
        {
            _HubService = ServiceContainer.Instance.GetServiceInstance(typeof(IHubService)) as HubService;
            
            RegisterMethods();
        }

        public async Task Login(LoginModel model, string hubUrl)
        {
            try
            {
                await _HubService.ConnectHub(hubUrl);

                if (_HubService.IsConnected)
                {
                    User = new User();
                    User.Name = model.Name;
                    User.Surname = model.Surname;

                    await _HubService.InvokeAsync(Commands.CONNECT_CLIENT, User);

                    User.ConnectionId = _HubService.GetConnectionId();
                    User.ConnectedDate = DateTime.Now;
                    User.UserStatus = UserStatus.Online;
                    User.IsConnect = true;

                    if (OnConnectedUser != null)
                    {
                        OnConnectedUser(new UserEventArgs(User));
                    }
                }
                else
                {
                    throw new Exception("Hub Servisi SignalR Hub'ına bağlı değil!");
                }

            }
            catch (Exception e)
            {

            }
        }

        public void LogOut()
        {

        }

        public void ChangeStatus(UserStatus newStatus)
        {
            User.UserStatus = newStatus;
            if (OnUserChangedStatus != null)
            {
                OnUserChangedStatus(new UserEventArgs(User)); // User'ın statüsü değiştiği için ChatUser listesindeki User'ı güncelle.
            }
        }

        private void RegisterMethods()
        {
            return;
        }

        public User User
        {
            get;
            private set;
        }
    }
}
