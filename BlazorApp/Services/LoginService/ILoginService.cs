using BlazorApp.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface ILoginService : IService
    {
        event UserEventHandler Connected;
        event UserEventHandler Disconnected;
        event UserEventHandler StatusChanged;

        Task Login(LoginModel user, string hubUrl);
        Task LogOut();
        Task ChangeStatus(UserStatus status);
        User User { get; }
    }
}
