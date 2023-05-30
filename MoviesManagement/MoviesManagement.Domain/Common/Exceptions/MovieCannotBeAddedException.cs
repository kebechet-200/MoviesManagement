using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieCannotBeAddedException : Exception
    {
        public const string Code = "Movie can not be added";

        public MovieCannotBeAddedException(string message) : base(message) { }
    }
}
