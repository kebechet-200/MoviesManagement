using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieCannotBeUpdatedException : Exception
    {
        public const string Code = "Movie can not be updated";

        public MovieCannotBeUpdatedException(string message) : base(message) { }
    }
}
