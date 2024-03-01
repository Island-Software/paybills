using System.Threading.Tasks;
using Paybills.API.Data;
using Paybills.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Paybills.API.Controllers
{
    public class TestingController : BaseApiController
    {
        private readonly IHostEnvironment _environment;
        private readonly DataContext _context;
        public TestingController(DataContext context, IHostEnvironment environment)
        {
            this._context = context;
            this._environment = environment;
        }

        [HttpPost("addbill")]
        public async Task<ActionResult> AddBill(UserBillDto userBillDto)
        {
            if (!_environment.IsDevelopment()) return BadRequest();            
            
            var user = await _context.Users
                .Include(u => u.Bills)
                .SingleAsync(u => u.Id == userBillDto.UserId);

            var bill = await _context.Bills
                .SingleAsync(b => b.Id == userBillDto.BillId);

            user.Bills.Add(bill);
            _context.SaveChanges();

            return Ok();
        }
    }
}