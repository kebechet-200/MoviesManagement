using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieStartsLessThanAnHourException : Exception
    {
        public const string Code = "Movie starts less than an hour.";

        public MovieStartsLessThanAnHourException(string message) : base(message) { }
    }
}
