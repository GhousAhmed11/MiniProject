using Microsoft.Extensions.Options;
using MiniProject.DTOs;
using MiniProject.Models;
using MiniProject.Repository.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MiniProject.Repository
{
    public class SendGridEmailSender
    {
        public SendGridEmailSender(
            IOptions<SendGridEmailSenderOptions> options
            )
        {
            this.Options = options.Value;
        }

        public SendGridEmailSenderOptions Options { get; set; }

        public async Task SendEmailAsync(SendEmailDTO sendEmailDTO)
        {
            await Execute(Options.ApiKey, sendEmailDTO.subject, sendEmailDTO.message, sendEmailDTO.email);
        }

        private async Task<Response> Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Options.SenderEmail, Options.SenderName);
            var sub = subject;
            var to = new EmailAddress(email);
            var plainText = message;
            var htmlText = message;
            var msg = MailHelper.CreateSingleEmail(from, to, sub, plainText, htmlText);
            var res = await client.SendEmailAsync(msg);
            return res;
        }
    }
}
