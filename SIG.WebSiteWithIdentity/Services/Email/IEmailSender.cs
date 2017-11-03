using System.Threading.Tasks;

namespace SIG.WebSiteWithIdentity.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
