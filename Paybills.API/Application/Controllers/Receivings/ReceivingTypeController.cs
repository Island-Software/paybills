using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paybills.API.Application.DTOs.ReceivingType;
using Paybills.API.Controllers;
using Paybills.API.Domain.Entities;
using Paybills.API.Domain.Services.Interfaces;

namespace Paybills.API.Application.Controllers
{
    [Authorize]
    public class ReceivingTypeController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IReceivingTypeService _service;

        public ReceivingTypeController(IReceivingTypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ReceivingTypeDto>> Create(ReceivingTypeRegisterDto receivingTypeRegisterDto)
        {
            if (await _service.Exists(receivingTypeRegisterDto.Description)) return BadRequest("Receiving type already exists");

            var newReceivingType = new ReceivingType
            {
                Description = receivingTypeRegisterDto.Description,
                Active = receivingTypeRegisterDto.Active
            };

            await _service.Create(newReceivingType);

            var result = _mapper.Map<ReceivingTypeDto>(newReceivingType);
            
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceivingTypeDto>>> GetAll() => Ok(_mapper.Map<IEnumerable<ReceivingTypeDto>>(await _service.GetAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceivingTypeDto>> Get(int id) => _mapper.Map<ReceivingTypeDto>(await _service.GetByIdAsync(id));

        [HttpGet]
        [Route("search/{description}")]
        public async Task<ActionResult<IEnumerable<ReceivingTypeDto>>> GetByDescription(string description) => Ok(await _service.GetByDescription(description));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ReceivingTypeDto receivingType)
        {
            if (!await TypeExists(id)) return NotFound();

            var repoReceivingType = await _service.GetByIdAsync(id);

            repoReceivingType.Description = repoReceivingType.Description;
            repoReceivingType.Active = repoReceivingType.Active;

            await _service.Update(repoReceivingType);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var receivingType = await _service.GetByIdAsync(id);

            if (receivingType == null) return NotFound();

            await _service.Delete(receivingType);

            return Ok();
        }

        private async Task<bool> TypeExists(int id)
        {
            return await _service.GetByIdAsync(id) != null;
        }
    }
}