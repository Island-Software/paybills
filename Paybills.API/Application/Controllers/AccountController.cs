using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Paybills.API.Domain.Services.Interfaces;

namespace Paybills.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private const int EXPIRATION_TIME_IN_DAYS = 7;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResultDto>> Register(RegisterDto registerDto)
        {
            if (await _userService.UserExistsAsync(registerDto.UserName)) 
                return BadRequest("Username already exists");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            
            await _userService.CreateAsync(user);

            return new LoginResultDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, EXPIRATION_TIME_IN_DAYS),
                UserId = user.Id
            };
        }

        [HttpPost("login")]        
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.GetUserByUserNameAsync(loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username/password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username/password");
            }

            return new LoginResultDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, EXPIRATION_TIME_IN_DAYS),
                UserId = user.Id
            };
        }
    }
}