using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.DTOs;
using Paybills.API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Paybills.API.Entities;

namespace Paybills.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private const int EMAIL_TOKEN_EXP_TIME_IN_DAYS = 1;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UsersController(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(id));
        }

        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string username)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetUserByUsernameWithDetails(username));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserDto userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            var validateEmail = user.Email != userDto.Email;

            user.Email = userDto.Email;
            if (validateEmail)
            {
                user.EmailToken = _tokenService.CreateToken(user, EMAIL_TOKEN_EXP_TIME_IN_DAYS);
                user.EmailValidated = false;
            }

            _userRepository.Update(user);

            if (validateEmail)
                SendEmailVerification(user);

            await _userRepository.SaveAllAsync();

            return Ok();
        }

        private void SendEmailVerification(AppUser user)
        {
            // var emailToken = 


        }
    }
}