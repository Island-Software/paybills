using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paybills.API.DTOs.User;
using Paybills.API.Services;

namespace Paybills.API.Controllers
{
    [Authorize]
    public class EmailController : BaseApiController
    {
        private readonly SESService _simpleEmailService;

        public EmailController(SESService simpleEmailService)
        {
            _simpleEmailService = simpleEmailService;

        }

        // [HttpPost]
        // [Route("verify")]
        // public async Task<ActionResult> VerifyEmail(EmailVerifyDto emailVerify)
        // {
        //     var email = emailVerify.Email;
        //     if (!string.IsNullOrWhiteSpace(email))
        //     {
        //         var success = await _simpleEmailService.VerifyEmailIdentityAsync(email);

        //         if (success)
        //             return Ok();
        //         else
        //         {
        //             return BadRequest($"Couldn't send email to verify {emailVerify.Email}");
        //         }
        //     }
        //     return BadRequest($"Invalid email: {emailVerify}");
        // }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendVerificationEmail(EmailVerifyDto emailVerify)
        {
            var success = await _simpleEmailService.SendEmailAsync(new List<string>() { emailVerify.Email }, null, null, "<h1>Testando</h1>", "", "teste de email", "admin@billminder.com.br");
            if (success != string.Empty)
            {
                return Ok();
            }
            else
            {
                return BadRequest($"Couldn't send email to nilsojr@gmail.com");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("validate")]
        public async Task<bool> ValidateEmail(string email, string emailToken)
        {
            var result = await Task.FromResult(true);
            return result;
        }

    }
}