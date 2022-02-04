using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IHubService : IService
    {
        Task ConnectHub(string hubUrl);
        Task DisconnectHub();
        Task InvokeAsync(string commandName, object obj);
        Task InvokeAsync(string commandName);
        HubConnection HubConnection { get; }
        bool IsConnected { get; }
        string GetConnectionId();
        void SubscribeCustomHubMethod<Tobj>(string commandName, Action<Tobj> method);
    }
}
