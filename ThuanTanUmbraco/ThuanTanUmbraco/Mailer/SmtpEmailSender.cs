using System.Net.Mail;
using System.Threading.Tasks;

namespace ThuanTanUmbraco.Mailer
{
    public class SmtpEmailSender: IEmailSender
    {
        private readonly SmtpClient _smtpClient;

        public SmtpEmailSender(string smtpHost, int port,
            bool useDefaultCredentials, string userName,
            string password, bool enableSsl)
        {
            _smtpClient = new SmtpClient
            {
                Host = smtpHost,
                Port = port,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new System.Net.NetworkCredential(userName, password),
                EnableSsl = enableSsl
            };
        }

        public void Send(EmailMessage message)
        {
            var mailMsg = MapFrom(message);

            try
            {
                _smtpClient.Send(mailMsg);
            }
            catch (SmtpException ex)
            {
                throw new EmailSenderException("Smtp", ex);
            }
        }

        public async Task SendAsync(EmailMessage message)
        {
            var mailMsg = MapFrom(message);

            try
            {
                await _smtpClient.SendMailAsync(mailMsg).ConfigureAwait(false);
            }
            catch (SmtpException ex)
            {
                throw new EmailSenderException("Smtp", ex);
            }
        }

        private static MailMessage MapFrom(EmailMessage message)
        {
            if (message == null)
            {
                throw new System.ArgumentNullException("Message can not be null");
            }

            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress(message.From, message.SenderDisplayName);

            // Add To addresses
            if (!string.IsNullOrEmpty(message.To))
            {
                foreach (var addr in message.To.Split(';'))
                {
                    mailMsg.To.Add(new MailAddress(addr));
                }
            }

            // Add Cc addresses
            if (!string.IsNullOrEmpty(message.Cc))
            {
                foreach (var addr in message.Cc.Split(';'))
                {
                    mailMsg.CC.Add(new MailAddress(addr));
                }
            }

            // Add attach files
            if (message.AttachFiles != null && message.AttachFiles.Count > 0)
            {
                foreach (var attFile in message.AttachFiles)
                {
                    mailMsg.Attachments.Add(new Attachment(attFile));
                }
            }
            if (message.AttachFileStream != null)
            {
                mailMsg.Attachments.Add(new Attachment(message.AttachFileStream.FileStream, message.AttachFileStream.FileName));
            }
            // subject
            mailMsg.Subject = message.Subject;
            mailMsg.Body = message.Body;
            mailMsg.IsBodyHtml = message.IsBodyHtml;

            return mailMsg;
        }
    }
}