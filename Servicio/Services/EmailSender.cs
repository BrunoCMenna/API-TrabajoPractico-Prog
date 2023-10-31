using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string toEmail, string userName, string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("proyecto.PPS.2023@gmail.com", "Tecno Rosario");
            var to = new EmailAddress(toEmail, userName);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
