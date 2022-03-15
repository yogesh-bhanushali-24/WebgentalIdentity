using System.Threading.Tasks;
using WebgentalIdentity.Models;

namespace WebgentalIdentity.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailConfirmationEmaill(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}