using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Common.Extensions
{
    internal static class CreateUserExtension
    {
        internal static User CreateUserModel(BaseUserModel request)
        {
            return new User
            {
                Username = request.Username,
                Password = EncryptPasswordExtension.Encrypt(request.Password)
            };
        }
    }
}
