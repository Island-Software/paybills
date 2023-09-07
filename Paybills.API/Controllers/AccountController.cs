using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Paybills.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IUserRepository userRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
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

            _userRepository.Create(user);
            await _userRepository.SaveAllAsync();

            return new LoginResultDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                UserId = user.Id
            };
        }

        private async Task<bool> UserExists(string userName) => await _userRepository.Exists(userName);

        [HttpPost("login")]        
        public async Task<ActionResult<LoginResultDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username/password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password/password");
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