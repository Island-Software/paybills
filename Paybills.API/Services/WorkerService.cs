using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Paybills.API.Data;

namespace Paybills.API.Services
{
    public class WorkerService
    {
        private SESService _simpleEmailService;

        public WorkerService(SESService simpleEmailService)
        {
            _simpleEmailService = simpleEmailService;
        }

        public async void StartWorker()
        {
            Console.WriteLine("Starting email sender service...");
            await ExecuteEmailSending();
            Console.WriteLine("Email sender service started");
        }

        private async Task ExecuteEmailSending()
        {
            while (true)
            {
                ProcessBills(GetBillsDue());

                await Task.Delay(TimeSpan.FromHours(1));
            }            
        }

        private List<BillDue> GetBillsDue()
        {
            var billsDue = new List<BillDue>();
            using (var context = new DataContext())
            {
                var initialDate = DateTime.Now;
                var finalDate = DateTime.Now.AddDays(3);

                var bills = context.Bills
                .Include(b => b.BillType)
                .Include(b => b.Users)
                .Where(b => b.DueDate > initialDate && b.DueDate < finalDate)
                .AsNoTracking()
                .ToList();

                foreach (var bill in bills)
                {
                    if (bill.Users.Take(1).FirstOrDefault().EmailValidated)
                    {
                        var userId = bill.Users.Take(1).FirstOrDefault().Id;
                        var userEmail = bill.Users.Take(1).FirstOrDefault().Email;
                        billsDue.Add(new BillDue(userId, bill.BillType.Description, userEmail, bill.DueDate));
                    }
                }
            }
            return billsDue;
        }

        private async void ProcessBills(List<BillDue> billsDue)
        {
            foreach (var bill in billsDue)
            {
                var result = await _simpleEmailService.SendEmailAsync(
                new List<string>() { bill.UserEmail }, 
                null, 
                null, 
                $"This is a reminder that you have bills near the due date: {bill.Description} - {bill.DueDate:d}",
                "", 
                "Billminder - bill(s) near due date", 
                "admin@billminder.com.br");
            }
        }
    }

    public class BillDue
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }
        public DateTime? DueDate { get; set; }

        public BillDue(int userId, string description, string userEmail, DateTime? dueDate)
        {
            UserId = userId;
            Description = description;
            DueDate = dueDate;
            UserEmail = userEmail;
        }
    }
}