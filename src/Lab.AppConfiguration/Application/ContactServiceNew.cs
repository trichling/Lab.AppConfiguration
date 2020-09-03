using System.Threading.Tasks;

namespace Lab.AppConfiguration.Application
{
    public class ContactServiceNew : IContactService
    {
        private readonly IEmailService emailService;
        public ContactServiceNew(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task SendContactRequest(string email, string message)
        {
            await emailService.SendMail("support@us.de", $"Request from {email}: {message}");
            await emailService.SendMail(email, $"Wir kümmern uns um deine Anfrage!");
        }
    }
}