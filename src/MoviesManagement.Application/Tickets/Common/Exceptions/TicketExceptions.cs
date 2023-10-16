using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Tickets.Common.Exceptions
{
    internal static class TicketExceptions
    {
        internal static class Throw
        {
            internal static TicketNotFoundException NotFound(string message) => 
                new($"{message} is empty");
            internal static InvalidStateException InvalidState(TicketEnum state) => 
                new($"Ticket status is not {state}");
            internal static YouAlreadyBoughtTicketException AlreadyBought() => 
                new("You already bought the ticket");
            internal static TicketNotBoughtException CannotBuy() => 
                new("The ticket can not be bought");
            internal static MovieAlreadyStartedException MovieAlreadyStarted() => 
                new("The movie that you are trying to buy already started.");
            internal static MovieIsInactiveException MovieInactive() =>
                new("The movie is inactive");
            internal static MovieStartsLessThanAnHourException MovieStartsSoon() =>
                new("You can't reserve the movie starts less than an hour.");
            internal static YouAlreadyReservedTicketException AlreadyReserved() =>
                new("You already reserve the ticket of this movie");
            internal static TicketNotReservedException CannotReserve() =>
                new("The ticket can not be reserved");
            internal static CantCancelTicketException NoActiveTicket() =>
                new("You don't have active tickets");
            internal static TicketNotCancelledException CannotCancel() =>
                new("The ticket can not be cancelled");
        }
    }
}
