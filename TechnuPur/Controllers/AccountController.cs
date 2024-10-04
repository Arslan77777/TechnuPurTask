using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechnuPur.DatabaseContext;
using TechnuPur.Model;

namespace TechnuPur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        IServiceScopeFactory factory;
        public AccountController(IConfiguration configuration, IServiceScopeFactory factory)
        {

            this.configuration = configuration;
            this.factory = factory;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] string Email,string Passsowrd)
        {
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = await context.Users.Where(f => f.Email == Email).FirstOrDefaultAsync();
            if(user is not null)
            {
               if(await userManager.CheckPasswordAsync(user, Passsowrd))
                {
                    return Ok(new { Toekn = GenerateJWTToken(user) });
                }
                return BadRequest("Password Wrong");
            }
            return NotFound("User Not Found");

        }
        /// <summary>
        /// this token get only for testing purpose main task is handle policies authentication etc 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        public async Task<IActionResult> GetToken()
        {
            User user = new User
            {
                Email = "arslanshafi2244@gmail.com",
                Role=Role.Admin,
                PhoneNumber="+487456784",
                UserName="Arslan",
                Id= Guid.NewGuid().ToString(),
               
            };

            
                    return Ok(new { Toekn = GenerateJWTToken(user) });
               

        }
        private async Task<dynamic> GenerateJWTToken(User user)
        {
            

            
            DateTime expiryDate = DateTime.UtcNow.AddMonths(1);
            var authClaims = new List<Claim>
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.NameIdentifier,user.Id),
                                new Claim(ClaimTypes.Name,user?.UserName),
                            };

            
                authClaims.Add(new Claim(ClaimTypes.MobilePhone,user.PhoneNumber));

           
                authClaims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
          

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                expires: expiryDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
               
            };
        }
    }
}
