using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserNotRegisteredException : Exception
    {
        public const string Code = "User could not register";

        public UserNotRegisteredException(string message) : base(message) { }
    }
}
