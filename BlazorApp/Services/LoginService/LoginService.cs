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

        HubService _HubService = null;

        public LoginService()
        {
            _HubService = ServiceContainer.Instance.GetServiceInstance(typeof(IHubService)) as HubService;
        }

        public async void Login(LoginModel model, string hubUrl)
        {
            try
            {
                await HubService.ConnectHub(hubUrl);

                if (HubService.IsConnected)
                {
                    User = new User();
                    User.Name = model.Name;
                    User.Surname = model.Surname;
                    //User.ImageUrl = model.ImageUrl;

                    await HubService.InvokeAsync(Commands.ConnectClient, User);

                    User.ConnectionId = HubService.ConnectionId;
                    User.ConnectedDate = DateTime.Now;
                    if (User.ConnectionId != null)
                        User.IsConnect = true;
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

        private HubService HubService
        {
            get
            {
                return _HubService;
            }
        }

        public User User
        {
            get;
            private set;
        }
    }
}
