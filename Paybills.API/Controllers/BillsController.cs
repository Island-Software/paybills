using System.Collections.Generic;
using System.Threading.Tasks;
using Paybills.API.DTOs;
using Paybills.API.Entities;
using Paybills.API.Extensions;
using Paybills.API.Helpers;
using Paybills.API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Paybills.API.Controllers
{
    [Authorize]
    public class BillsController : BaseApiController
    {
        private readonly IBillRepository _billsRepository;
        private readonly IBillTypeRepository _billTypesRepository;
        private readonly IMapper _mapper;

        public BillsController(IBillRepository billsRepository, IBillTypeRepository billTypesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _billsRepository = billsRepository;
            _billTypesRepository = billTypesRepository;
        }

        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<IEnumerable<BillDto>>> GetBills(string username, [FromQuery] UserParams userParams)
        {
            var bills = await _billsRepository.GetBillsAsync(username, userParams);

            Response.AddPaginationHeader(bills.CurrentPage, bills.PageSize, bills.TotalCount, bills.TotalPages);

            var billsToReturn = _mapper.Map<IEnumerable<BillDto>>(bills);            

            return Ok(billsToReturn);
        }

        [HttpGet]
        [Route("name/{username}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<BillDto>>> GetBillsByDate(string username, int month, int year, [FromQuery] UserParams userParams)
        {
            var bills = await _billsRepository.GetBillsByDateAsync(username, month, year, userParams);

            Response.AddPaginationHeader(bills.CurrentPage, bills.PageSize, bills.TotalCount, bills.TotalPages);

            var billsToReturn = _mapper.Map<IEnumerable<BillDto>>(bills);            

            return Ok(billsToReturn);
        }

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
        public async Task<ActionResult<BillDto>> Create(BillRegisterDto bill)
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
            await _billsRepository.AddBillToUser(bill.UserId, newBill.Id);
            await _billsRepository.SaveAllAsync();

            var billToReturn = _mapper.Map<BillDto>(newBill);
            return billToReturn;
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

        [HttpPost("copy")]
        public async Task<ActionResult> CopyBillsToNextMonth(PeriodDataDto periodData)
        {
            var bills = await _billsRepository.CopyBillsToNextMonth(periodData.UserId, periodData.CurrentMonth, periodData.CurrentYear);            

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