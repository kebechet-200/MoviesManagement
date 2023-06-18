using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Common.Extensions
{
    public static class TicketCommandToDomainExtension
    {
        public static Ticket TicketCommandToDomain(this BaseTicketModel model) =>
            new Ticket
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                State = model.State
            };
    }
}
