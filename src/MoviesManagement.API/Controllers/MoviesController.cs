using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Movies.Queries.GetAll;
using System.Net;

namespace MoviesManagement.API.Controllers
{
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
        public async Task<IActionResult> Create([FromBody] CreateMovieCommand command) => Ok(await _mediator.Send(command));

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateMovieCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteMovieCommand command) => Ok(await _mediator.Send(command));

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] GetMovieQuery command) => Ok(await _mediator.Send(command));

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(GetAllMoviesQuery command) => Ok(await _mediator.Send(command)); // TODO Lasha : test if it works, because this endpoint needs no parameter.
    }
}
