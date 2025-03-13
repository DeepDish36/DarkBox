using DarkBox.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DarkBox.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        // 📌 Enviar Mensagem para um Usuário Específico
        public async Task SendMessage(string senderUsername, string message, int recipientId, int? projectId = null)
        {
            var sender = _context.Users.FirstOrDefault(u => u.Username == senderUsername);

            if (sender == null)
            {
                Console.WriteLine($"Erro: Remetente {senderUsername} não encontrado no banco de dados.");
                throw new Exception("Remetente não encontrado.");
            }

            var recipient = _context.Users.FirstOrDefault(u => u.UserId == recipientId);
            if (recipient == null)
            {
                throw new Exception("Destinatário não encontrado.");
            }

            var chatMessage = new Message
            {
                SenderId = sender.UserId,
                ReceiverId = recipient.UserId,
                MessageText = message,
                SentAt = DateTime.UtcNow,
                ProjectId = projectId  // Definir o ProjectId se fornecido
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.User(recipientId.ToString()).SendAsync("ReceiveMessage", senderUsername, message);
        }

        // 📌 Buscar Usuários em Tempo Real
        public async Task SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                await Clients.Caller.SendAsync("ReceiveUserResults", new List<object>());
                return;
            }

            var users = _context.Users
                .Where(u => u.Username.Contains(searchTerm) || u.Email.Contains(searchTerm))
                .Select(u => new { u.UserId, u.Username, u.Email })
                .Take(10)  // Limita para evitar sobrecarga
                .ToList();

            await Clients.Caller.SendAsync("ReceiveUserResults", users);
        }
    }
}
