using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PoriectSD.Models;
using PoriectSD.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PoriectSD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthServices _authService;
        public AuthController(IAuthServices authServices)
        {
            _authService = authServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            var data =  await _authService.Login(user);
            if(data != null)
            {
                return Ok(data);
            }
            return Unauthorized();
        }
    }
}
