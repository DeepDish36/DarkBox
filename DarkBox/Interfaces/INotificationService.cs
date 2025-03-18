using DarkBox.Hubs;
using DarkBox.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DarkBox.Interfaces
{
    public interface INotificationService
    {
        Task SendNotification(int userId, string message);
    }
}
