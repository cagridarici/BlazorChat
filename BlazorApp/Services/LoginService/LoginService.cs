﻿using BlazorApp.Models;
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
        public event UserEventHandler Connected  = null;
        public event UserEventHandler Disconnected = null;
        public event UserEventHandler StatusChanged = null;

        IHubService _HubService = null;

        public LoginService()
        {
            _HubService = ServiceContainer.Instance.GetServiceInstance(typeof(IHubService)) as IHubService;
            SubscribeHubMethods();
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
                    User.UserStatus = UserStatus.Online;
                    User.ConnectedDate = DateTime.Now;
                    User.IsConnect = true;

                    await _HubService.InvokeAsync(HubCommands.CONNECT_CLIENT, User);

                    User.IsConnect = true;
                    User.ConnectionId = _HubService.GetConnectionId();

                    OnConnected();
                }
                else
                {
                    throw new Exception("Hub Servisi SignalR Hub'ına bağlı değil!");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task LogOut()
        {
            try
            {
                await _HubService.InvokeAsync(HubCommands.DISCONNECT_CLIENT, User);
                //await _HubService.DisconnectHub();
                User = null;
                OnDisconnected();
            }
            catch (Exception e)
            {
                
            }
        }

        public async Task ChangeStatus(UserStatus newStatus)
        {
            User.UserStatus = newStatus;
            await _HubService.InvokeAsync(HubCommands.CHANGE_USER_STATUS, User);
            OnStatusChanged();
        }

        public void GetChangedUser(User user)
        {

        }

        private void OnDisconnected()
        {
            if (Disconnected != null)
                Disconnected(new UserEventArgs(User));
        }

        private void OnConnected()
        {
            if (Connected != null)
                Connected(new UserEventArgs(User));
        }

        protected override void SubscribeHubMethods()
        {
            if (!_HubService.IsConnected)
                return;
            base.SubscribeHubMethods();
        }

        private void OnStatusChanged()
        {
            if (StatusChanged != null)
                StatusChanged(new UserEventArgs(User)); // User'ın statüsü değiştiği için ChatUser listesindeki User'ı güncelle.
        }

        public User User
        {
            get;
            private set;
        }

        public async override void Dispose()
        {
            base.Dispose();
            if (User != null)
            {
                await LogOut();
            }
        }
    }
}
