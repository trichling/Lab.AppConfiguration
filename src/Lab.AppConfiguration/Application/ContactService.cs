using System.Threading.Tasks;

namespace Lab.AppConfiguration.Application
{
    public class ContactService : IContactService
    {
        private readonly IEmailService emailService;
        public ContactService(IEmailService emailService)
        {
            this.emailService = emailService;

        }
        public async Task SendContactRequest(string email, string message)
        {
            await emailService.SendMail("trashcan@us.de", string.Empty);
        }

    }
}