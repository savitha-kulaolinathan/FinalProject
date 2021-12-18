using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinalProject.Service
{
    public class EmailSender : IEmailSender { 
        public EmailOptions options { get; set; }
    
        public EmailSender(IOptions<EmailOptions> emailoptions)
        {
          options = emailoptions.Value;
        }
        public Task SendEmailAsync(string email, string subject,string message)
        {
            return Excute(options.SendGridKey, subject, message, email);
        }

        private Task Excute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("ss@bp.com", "book"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message

            };
            msg.AddTo(new EmailAddress(email));
            try
            {
                return client.SendEmailAsync(msg);

            }
            catch(Exception)
            {

            }
            return null;
        }
    }
}
