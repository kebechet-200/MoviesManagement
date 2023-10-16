using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Movies.Queries.GetAll;
using MoviesManagement.Application.Tickets.Commands.Buy;
using MoviesManagement.Application.Tickets.Commands.Cancel;
using MoviesManagement.Application.Tickets.Commands.Reserve;
using MoviesManagement.Application.Tickets.Queries.Get;
using MoviesManagement.Application.Tickets.Queries.GetAll;
using MoviesManagement.Application.Users.Commands.Create;
using MoviesManagement.Application.Users.Commands.Delete;
using MoviesManagement.Application.Users.Commands.Update;
using System.Net;

namespace MoviesManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BuyTicketCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ReserveTicketCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] CancelTicketCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] GetTicketQuery query, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(GetAllTicketQuery query, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(query, cancellationToken));
    }
}
