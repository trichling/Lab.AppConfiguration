using System.Threading.Tasks;

namespace Lab.AppConfiguration.Application
{
    public interface IContactService
    {
         
        Task SendContactRequest(string email, string message);

    }
}