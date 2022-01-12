using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    [Guid("14FA44E4-0F69-4A40-8A64-65FC4ECD64EC")]
    public class HubService : ServiceBase, IHubService
    {
        HubConnection _HubConnection = null;
        bool _IsConnected = false;
        string _ConnectionId = string.Empty;

        public event HubConnectionEventHandler OnConnected = null;

        public HubService()
        {

        }

        public async Task ConnectHub(string hubUrl)
        {
            try
            {
                _HubConnection = new HubConnectionBuilder().WithUrl(hubUrl).Build();
                await _HubConnection.StartAsync();
                _IsConnected = true;
                RegisterMethods();
            }
            catch (Exception e)
            {
                _IsConnected = false;
                throw new Exception(string.Format("SignalR Hub'ı ile bağlantı kurulamadı. Hata Detayı : {0} ", e.Message));
            }
        }

        public async Task InvokeAsync(string commandName, object obj)
        {
            //ConnectHub();
            if (!IsConnected)
                throw new Exception("Hub Servisi SignalR Hub'ına Bağlı Değil ! ");
            await _HubConnection.InvokeAsync(commandName, obj);
        }

        private void RegisterMethods()
        {
            if (!IsConnected)
                throw new Exception("Hub Servisi SignalR Hub'ına Bağlı Değil ! ");
            _HubConnection.On<string>(Commands.GET_CONNECTION_ID, this.GetConnectionId);
        }

        /// <summary>
        /// Get Connection Id from SignalR Hub
        /// </summary>
        /// <param name="connectionId"></param>
        private void GetConnectionId(string connectionId)
        {
            _ConnectionId = connectionId;
            if (OnConnected != null)
            {
                OnConnected(connectionId);
            }
        }

        public async Task DisconnectHub()
        {
            await _HubConnection.DisposeAsync();
            _HubConnection = null;
            _IsConnected = false;
        }

        public string GetConnectionId()
        {
            return _ConnectionId;
        }

        public HubConnection HubConnection
        {
            get { return _HubConnection; }
        }

        public bool IsConnected
        {
            get { return _IsConnected; }
        }
    }
}
