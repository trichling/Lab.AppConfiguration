using System.Threading.Tasks;

namespace Lab.AppConfiguration.Application
{
    public interface IEmailService
    {
        Task SendMail(string receipient, string body);
    }

    public class EmailService : IEmailService
    {
        public Task SendMail(string receipient, string body)
        {
            return Task.CompletedTask;
        }
    }
}