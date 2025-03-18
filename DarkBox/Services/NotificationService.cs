using DarkBox.Hubs;
using DarkBox.Interfaces;
using DarkBox.Models;
using Microsoft.AspNetCore.SignalR;

namespace DarkBox.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationService(AppDbContext context, IHubContext<NotificationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                NotificationText = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
