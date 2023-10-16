using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Movies.Queries.GetAll;
using System.Net;

namespace MoviesManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateMovieCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteMovieCommand command, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(command, cancellationToken));

        [HttpGet("{query.Id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] GetMovieQuery query, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(query, cancellationToken));

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(GetAllMoviesQuery query, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(query, cancellationToken));
    }
}
