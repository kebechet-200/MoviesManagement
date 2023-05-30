using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class InvalidUserException : Exception
    {
        public const string Code = "Invalid user.";

        public InvalidUserException(string message) : base(message) { }
    }
}
