using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public const string Code = "User Already Exists";
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
