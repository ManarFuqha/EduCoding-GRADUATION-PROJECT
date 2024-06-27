
using System;
using System.Threading.Tasks;
using courseProject.Core.IGenericRepository;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace courseProject.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }






        // Public method to send a verification email
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            // Create the email message
            var message = CreateEmailMessage(toEmail, subject, body);

            // Send the email message using an SMTP client
            using (var client = new SmtpClient())
            {
                await SendAsync(client, message);
            }
        }

        // Private method to create the email message
        private MimeMessage CreateEmailMessage(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();

            // Set the sender's email address and display name
            message.From.Add(new MailboxAddress("EduCoding Academy", _emailConfig.From));

            // Set the recipient's email address
            message.To.Add(new MailboxAddress("", toEmail));

            // Set the subject of the email
            message.Subject = subject;

            // Create the body of the email with HTML content
            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        // Private method to send the email message using the SMTP client
        private async Task SendAsync(SmtpClient client, MimeMessage message)
        {
            try
            {
                // Connect to the SMTP server using SSL/TLS encryption
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate using the sender's email address and password
                await client.AuthenticateAsync(_emailConfig.From, _emailConfig.Password);

                // Send the email message
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the sending process
                throw;
            }
            finally
            {
                // Ensure the client disconnects from the SMTP server
                await client.DisconnectAsync(true);
            }
        }
    }



}
        


       

    


