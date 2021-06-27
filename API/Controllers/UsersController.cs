using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() => Ok(await _userRepository.GetUsersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) => await _userRepository.GetUserByIdAsync(id);

        [HttpGet("{username}")]
        public async Task<ActionResult<AppUser>> GetUserByName(string username) => await _userRepository.GetUserByUsernameAsync(username);
    }
}