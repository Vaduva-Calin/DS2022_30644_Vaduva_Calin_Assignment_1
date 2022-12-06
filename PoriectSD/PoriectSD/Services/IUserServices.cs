using PoriectSD.Models;

namespace PoriectSD.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetUsers();
        Task AddUser(User user);
        Task RemoveUser(int user);
        Task EditUser(User user);
    }
}
