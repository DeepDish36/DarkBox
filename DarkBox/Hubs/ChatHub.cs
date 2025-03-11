using DarkBox.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DarkBox.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message, int recipientId)
        {
            var sender = _context.Users.FirstOrDefault(u => u.Username == user);
            if (sender == null)
            {
                throw new Exception("Sender not found");
            }

            var recipient = _context.Users.FirstOrDefault(u => u.UserId == recipientId);
            if (recipient == null)
            {
                throw new Exception("Recipient not found");
            }

            var newMessage = new Message
            {
                SenderId = sender.UserId,
                ReceiverId = recipient.UserId,
                MessageText = message,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            await Clients.User(recipient.UserId.ToString()).SendAsync("ReceiveMessage", user, message);
        }
    }
}

