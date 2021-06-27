using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class BillController : BaseApiController
    {
        private readonly IBillRepository _billsRepository;
        private readonly IBillTypeRepository _billTypesRepository;

        public BillController(IBillRepository billsRepository, IBillTypeRepository billTypesRepository)
        {
            this._billsRepository = billsRepository;
            this._billTypesRepository = billTypesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills() => Ok(await _billsRepository.GetBillsAsync());        

        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id) => await _billsRepository.GetBillByIdAsync(id);

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bill = await _billsRepository.GetBillByIdAsync(id);

            if (bill == null) return NotFound();

            _billsRepository.Delete(bill);
            await _billsRepository.SaveAllAsync();

            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Bill>> Create(BillRegisterDto bill)
        {
            var billType = await _billTypesRepository.GetBillTypeByIdAsync(bill.TypeId);

            if (billType == null) return BadRequest($"Bill type of id {bill.TypeId} not found");

            var newBill = new Bill
            {
                BillType = billType,
                Month = bill.Month,
                Year = bill.Year,
                Value = bill.Value
            };

            _billsRepository.Create(newBill);
            await _billsRepository.SaveAllAsync();

            return newBill;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BillRegisterDto bill)
        {
            if (!await BillExists(id))
                return NotFound();

            var repoBill = await _billsRepository.GetBillByIdAsync(id);

            // TO-DO: add automapper to project
            repoBill.Value = bill.Value;
            repoBill.Month = bill.Month;
            repoBill.Year = bill.Year;

            await _billsRepository.SaveAllAsync();

            return Ok();
        }

        // TO-DO: create an out property for the found object
        private async Task<bool> BillExists(int id)
        {
            return await _billsRepository.GetBillByIdAsync(id) != null;
        }
    }
}