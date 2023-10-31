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
