using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersToReturn);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id) => _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(id));

        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string username) => _mapper.Map<UserDto>(await _userRepository.GetUserByUsernameAsync(username));
    }
}