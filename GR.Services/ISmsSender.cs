using System.Threading.Tasks;

namespace GR.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
