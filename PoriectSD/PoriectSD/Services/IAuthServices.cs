using PoriectSD.Models;

namespace PoriectSD.Services
{
    public interface IAuthServices
    {
        Task<Object> Login(User user);
    }
}