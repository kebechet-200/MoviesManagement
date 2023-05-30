using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserNotUpdatedException : Exception
    {
        public const string Code = "User not updated";

        public UserNotUpdatedException(string message) : base(message) { }
    }
}
