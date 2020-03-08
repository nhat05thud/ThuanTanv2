using System.Threading.Tasks;

namespace ThuanTanUmbraco.Mailer
{
    public interface IEmailSender
    {
        void Send(EmailMessage message);

        Task SendAsync(EmailMessage message);
    }
}