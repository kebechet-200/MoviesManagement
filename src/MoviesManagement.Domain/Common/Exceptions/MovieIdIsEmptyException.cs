using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieIdIsEmptyException : Exception
    {
        public const string Code = "Movie id is empty";

        public MovieIdIsEmptyException(string message) : base(message) { }
    }
}
