﻿using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Tickets.Commands.Buy
{
    public class BuyTicketCommandHandler : IRequestHandler<BuyTicketCommand, Unit>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public BuyTicketCommandHandler(ITicketRepository ticketRepository,
                                          IMovieRepository movieRepository,
                                          IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new TicketNotFoundException("Userid is empty");

            if (request.MovieId == Guid.Empty)
                throw new TicketNotFoundException("MovieId is empty");

            if (request.State is not TicketEnum.Buy)
                throw new InvalidStateException("Ticket status is not Buy");

            var user = await _userRepository.GetAsync(request.UserId).ConfigureAwait(false);
            var movie = await _movieRepository.GetAsync(request.MovieId).ConfigureAwait(false);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");


            if (movie.IsActive is false)
                throw new MovieIsInactiveException("The movie is inactive");
            
            bool isMovieStartedAlready = movie.IsExpired || DateTime.UtcNow > movie.StartDate;

            if (isMovieStartedAlready)
                throw new MovieAlreadyStartedException("The movie that you are trying to buy already started.");

            var movieTickets = user.Tickets
                .Where(x => x.UserId == user.Id)
                .Where(x => x.MovieId == movie.Id);

            if (movieTickets.Any(x => x.State == TicketEnum.Buy))
                throw new YouAlreadyBoughtTicketException("You already bought the ticket");

            //TODO : ეს მიდგომა არ მომწონს, ვერ ვიგებ იყიდა ბილეთი თუ ექსეფშენი ისროლა try/catch ან logging (უმჯობესია ორივე)
            await _ticketRepository.BuyTicketAsync(request.TicketCommandToDomain()).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
