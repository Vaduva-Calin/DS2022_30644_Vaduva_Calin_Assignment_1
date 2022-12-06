
using PoriectSD.Database;
using PoriectSD.Models;
using PoriectSD.Services;

namespace PoriectSD.Services
{
    public class UserServices : IUserServices
    {
        SDDbContext _dbContext;

        public UserServices(SDDbContext SDDbContext)
        {
            _dbContext = SDDbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public async Task AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditUser(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveUser(int userId)
        {
            try
            {
                User user= new User() { Id = userId };
                _dbContext.Users.Attach(user);
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
