using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ODT_Model.DTO.Response;
using ODT_Repository;
using ODT_Service.Interface;


namespace ODT_Service
{
    public class EmailConfig : IEmailConfig
    {
        private readonly Email _emailSettings;

        public EmailConfig(IOptions<Email> emailSettings)
        {
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        }

        public ResponseDTO SendEmail(EmailDTO emailDTO)
        {
            var emailMessage = CreateEmailMessage(emailDTO);
            Send(emailMessage);

            return new ResponseDTO
            {
                Success = true,
                Message = "Email sent successfully"
            };
        }

        private MimeMessage CreateEmailMessage(EmailDTO emailDTO)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("TamNguyenDev", _emailSettings.From));
            emailMessage.To.AddRange(emailDTO.To);
            emailMessage.Subject = emailDTO.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailDTO.Body };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailSettings.UserName, _emailSettings.Password);

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending the email: " + ex.Message, ex);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}