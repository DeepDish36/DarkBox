using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DarkBox.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
