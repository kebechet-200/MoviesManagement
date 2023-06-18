using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieIsInactiveException : Exception
    {
        public const string Code = "The movie is inactive";

        public MovieIsInactiveException(string message) : base(message) { }
    }
}
