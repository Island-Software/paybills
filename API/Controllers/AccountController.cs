using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public AccountController(DataContext context, ITokenService tokenService, IConfiguration config,
            IWebHostEnvironment env)
        {
            this._config = config;
            this._env = env;
            this._tokenService = tokenService;
            this._context = context;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<LoginResultDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) 
                return BadRequest("Username already exists");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new LoginResultDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                UserId = user.Id
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _context.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }

        [HttpPost("login")]        
        // [EnableCors]
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new LoginResultDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                UserId = user.Id
            };
        }
    }
}