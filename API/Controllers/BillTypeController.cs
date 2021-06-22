using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class BillTypeController : BaseApiController
    {
        private readonly DataContext _context;

        public BillTypeController(DataContext context)
        {
            this._context = context;
        }        

        [HttpPost("create")]
        public async Task<ActionResult<BillType>> Create(BillTypeDto billType)
        {
            if (await TypeExists(billType.Description)) return BadRequest("Bill type already exists");
            
            var newBillType = new BillType
            {
                Description = billType.Description,
                Active = billType.Active
            };

            _context.BillTypes.Add(newBillType);
            await _context.SaveChangesAsync();

            return newBillType;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillType>>> GetBillTypes() => await _context.BillTypes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BillType>> GetBillType(int id) => await _context.BillTypes.FindAsync(id);

        [HttpGet]
        [Route("search/{description}")]        
        public async Task<ActionResult<IEnumerable<BillType>>> GetBillTypesByDescription(string description)
        {
            return await _context.BillTypes.Where(bt => bt.Description.ToLower().Contains(description.ToLower())).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BillType billType)
        {
            if (!await TypeExists(id))
                return NotFound();

            var repoBillType = await _context.BillTypes.FindAsync(id);

            repoBillType.Description = billType.Description;
            repoBillType.Active = billType.Active;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {        
            var billType = await _context.BillTypes.FindAsync(id);

            if (billType == null) return NotFound();

            _context.BillTypes.Remove(billType);
            await _context.SaveChangesAsync();
            
            return Ok();
        }        
        private async Task<bool> TypeExists(int id)
        {
            return await _context.BillTypes.FindAsync(id) != null;
        }

        private async Task<bool> TypeExists(string description)
        {
            return await _context.BillTypes.AnyAsync(type => type.Description.ToLower() == description.ToLower());
        }
    }
}