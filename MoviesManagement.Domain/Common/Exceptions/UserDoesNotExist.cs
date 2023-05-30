using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public const string Code = "User does not exist.";

        public UserDoesNotExistException(string message) : base(message) { }
    }
}
