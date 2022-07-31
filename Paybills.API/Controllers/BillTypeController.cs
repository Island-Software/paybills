using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Paybills.API.Controllers
{
    [Authorize]
    public class BillTypeController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IBillTypeRepository _repository;

        public BillTypeController(IBillTypeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<BillTypeDto>> Create(BillTypeRegisterDto billType)
        {
            if (await TypeExists(billType.Description)) return BadRequest("Bill type already exists");

            var newBillType = new BillType
            {
                Description = billType.Description,
                Active = billType.Active
            };

            _repository.Create(newBillType);
            await _repository.SaveAllAsync();

            return _mapper.Map<BillTypeDto>(newBillType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillTypeDto>>> GetBillTypes() => Ok(_mapper.Map<IEnumerable<BillTypeDto>>(await _repository.GetBillTypesAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<BillTypeDto>> GetBillType(int id) => _mapper.Map<BillTypeDto>(await _repository.GetBillTypeByIdAsync(id));

        [HttpGet]
        [Route("search/{description}")]
        public async Task<ActionResult<IEnumerable<BillTypeDto>>> GetBillTypesByDescription(string description) => Ok(await _repository.GetBillTypeByDescription(description));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BillTypeDto billType)
        {
            if (!await TypeExists(id)) return NotFound();

            var repoBillType = await _repository.GetBillTypeByIdAsync(id);

            repoBillType.Description = billType.Description;
            repoBillType.Active = billType.Active;

            await _repository.SaveAllAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var billType = await _repository.GetBillTypeByIdAsync(id);

            if (billType == null) return NotFound();

            _repository.Delete(billType);
            await _repository.SaveAllAsync();

            return Ok();
        }
        private async Task<bool> TypeExists(int id)
        {
            return await _repository.GetBillTypeByIdAsync(id) != null;
        }

        private async Task<bool> TypeExists(string description)
        {                    
            return await _repository.BillTypeExists(description);
        }
    }
}