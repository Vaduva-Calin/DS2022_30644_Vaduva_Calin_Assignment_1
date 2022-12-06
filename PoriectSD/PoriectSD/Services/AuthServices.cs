using Microsoft.IdentityModel.Tokens;
using PoriectSD.Database;
using PoriectSD.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PoriectSD.Services
{
    public class AuthServices : IAuthServices
    {
        SDDbContext _dbContext;
        public AuthServices(SDDbContext SDDbContext)
        {
            _dbContext = SDDbContext;
        }
        public async Task<Object> Login(User user)
        {
            try
            {
                Console.Write(user.Name);
                var u = _dbContext.Users.FirstOrDefault(us => us.UserName == user.UserName && us.Password == user.Password);
                
                if (u != null)
                {
                    
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, u.Role)
                    };
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "https://localhost:4200",
                        audience: "https://localhost:4200",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    var token = new AuthenticatedResponse { Token = tokenString };
                    var response = new {token,u.Role, u.Id};
                    return response;
                }
                throw new ArgumentNullException("No user found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        }
    }
