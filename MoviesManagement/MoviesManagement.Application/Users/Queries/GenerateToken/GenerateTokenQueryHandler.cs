using MediatR;
using Microsoft.Extensions.Logging;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Application.Common;
using MoviesManagement.Domain.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MoviesManagement.Application.Users.Queries.GenerateToken
{
    public class GenerateTokenQueryHandler : IRequestHandler<GenerateTokenQuery, string>
    {
        private IUserRepository _userRepository;
        private ILogger<GenerateTokenQueryHandler> _logger;

        public GenerateTokenQueryHandler(IUserRepository userRepository, ILogger<GenerateTokenQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<string> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException("The operation has been cancelled");

                await ValidateUserExtension.ValidateQuery(request, cancellationToken);

                var userExists = await _userRepository.Exists(request.Username, cancellationToken);

                if (userExists is false)
                    throw new UserDoesNotExistException($"The user with name {request.Username} does not exist");

                var user = CreateUserExtension.CreateUserModel(request);

                Guid? userId = await _userRepository.Validate(user, cancellationToken);

                if (userId.HasValue is false)
                    throw new InvalidUserException($"Username or password is invalid");

                var token = GenerateSecurityToken(userId.Value);

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return ex.Message;
            }
        }

        // TODO : Implement token generation using jwt handler
        private string GenerateSecurityToken(Guid guid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SUPER-SECRET-PASSWORD");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, guid.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
