using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MoviesNotFoundException : Exception
    {
        public const string Code = "Movies not found";

        public MoviesNotFoundException(string message) : base(message) { }
    }
}
