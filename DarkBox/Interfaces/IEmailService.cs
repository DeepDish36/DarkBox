using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace DarkBox.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
