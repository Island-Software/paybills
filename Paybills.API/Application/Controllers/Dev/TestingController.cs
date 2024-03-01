using Paybills.API.Data;
using Microsoft.Extensions.Hosting;

namespace Paybills.API.Controllers
{
    public class TestingController : BaseApiController
    {
        private readonly IHostEnvironment _environment;
        private readonly DataContext _context;
        public TestingController(DataContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
    }
}