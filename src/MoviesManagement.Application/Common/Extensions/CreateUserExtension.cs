using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.POCO;

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
