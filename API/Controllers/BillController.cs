using System;
using System.Collections.Generic;
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
    public class BillController : BaseApiController
    {
        private readonly DataContext _context;

        public BillController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            return await _context.Bills.Include(b => b.BillType).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id) => await _context.Bills.FindAsync(id);

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {        
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null) return NotFound();

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Bill>> Create(BillDto bill)
        {               
            var billType = await _context.BillTypes.FindAsync(bill.TypeId);

            if (billType == null) return BadRequest($"Bill type of id {bill.TypeId} not found");

            var newBill = new Bill
            {
                BillType = billType,
                Month = bill.Month,
                Year = bill.Year,
                Value = bill.Value
            };

            _context.Bills.Add(newBill);
            await _context.SaveChangesAsync();

            return newBill;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BillDto bill)
        {
            if (!await BillExists(id))
                return NotFound();

            var repoBill = await _context.Bills.FindAsync(id);

            repoBill.Value = bill.Value;
            repoBill.Month = bill.Month;
            repoBill.Year = bill.Year;
        
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> BillExists(int id)
        {
            return await _context.Bills.FindAsync(id) != null;
        }
    }
}