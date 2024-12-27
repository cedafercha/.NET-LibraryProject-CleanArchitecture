using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LibraryProject.Application.Commands;
using LibraryProject.Application.Queries;

namespace LibraryProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateNewLoan(NewLoanCommand newLoanCommandcommand)
        {
            var result = await _mediator.Send(newLoanCommandcommand);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLoanQuery { LoanId = id }).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery()).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
