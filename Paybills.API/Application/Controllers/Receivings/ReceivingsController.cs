using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Paybills.API.Application.DTOs.Receiving;
using Paybills.API.Controllers;
using Paybills.API.Domain.Services.Interfaces;
using Paybills.API.Extensions;
using Paybills.API.Helpers;

namespace Paybills.API.Application.Controllers
{
    public class ReceivingsController : BaseApiController
    {
        private readonly IReceivingService _receivingService;
        private readonly IMapper _mapper;

        public ReceivingsController(IReceivingService receivingService, IMapper mapper)
        {
            _receivingService = receivingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("name/{username}")]
        public async Task<ActionResult<IEnumerable<ReceivingDto>>> GetReceivings(string username, [FromQuery] UserParams userParams)
        {
            // Simulate fetching data from a service
            var receivings = await _receivingService.GetAsync(username, userParams);

            // Add pagination header to the response
            Response.AddPaginationHeader(receivings.CurrentPage, receivings.PageSize, receivings.TotalCount, receivings.TotalPages);

            // Map the data to DTOs
            var receivingsToReturn = _mapper.Map<IEnumerable<ReceivingDto>>(receivings);

            return Ok(receivingsToReturn);
        }
    }
}