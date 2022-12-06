using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PoriectSD.Models;
using PoriectSD.Services;

namespace PoriectSD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserServices _userService;
        public UserController(IUserServices userServices)
        {
            _userService = userServices;
        }

        [HttpGet("get")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPost("add")]
        public async Task AddUser(User user)
        {
            await _userService.AddUser(user);
        }

        [HttpPut("edit")]
        public async Task EditUser(User user)
        {
            await _userService.EditUser(user);
        }

        [HttpDelete("remove")]
        public async Task RemoveUser(int userId)
        {
            await _userService.RemoveUser(userId);
        }

       
    }
}