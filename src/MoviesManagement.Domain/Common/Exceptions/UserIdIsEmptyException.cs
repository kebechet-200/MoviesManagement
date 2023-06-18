using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserIdIsEmptyException : Exception
    {
        public const string Code = "User id is empty";

        public UserIdIsEmptyException(string message) : base(message) { }
    }
}
