using Microsoft.Extensions.DependencyInjection;
using Moq;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Queries.GetAll;
using MoviesManagement.Application.Users.Commands.Create;
using MoviesManagement.Application.Users.Commands.Delete;
using MoviesManagement.Application.Users.Commands.Update;
using MoviesManagement.Application.Users.Queries.Get;
using MoviesManagement.Application.Users.Queries.GetAll;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tests.Fixtures
{
    public class UserFixture
    {
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private ServiceCollection ServiceCollection { get; } = new ServiceCollection();

        public CreateUserCommandHandler CreateUserCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                CreateUserCommandHandler service = serviceProvider.GetRequiredService<CreateUserCommandHandler>();
                return service;
            }
        }

        public UpdateUserCommandHandler UpdateUserCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                UpdateUserCommandHandler service = serviceProvider.GetRequiredService<UpdateUserCommandHandler>();
                return service;
            }
        }

        public DeleteUserCommandHandler DeleteUserCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                DeleteUserCommandHandler service = serviceProvider.GetRequiredService<DeleteUserCommandHandler>();
                return service;
            }
        }

        public GetUserQueryHandler GetUserQueryHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                GetUserQueryHandler service = serviceProvider.GetRequiredService<GetUserQueryHandler>();
                return service;
            }
        }

        public GetAllUserQueryHandler GetAllUserQueryHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                GetAllUserQueryHandler service = serviceProvider.GetRequiredService<GetAllUserQueryHandler>();
                return service;
            }
        }

        public UserFixture()
        {
            var successGuid = new Guid("{CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C}");
            var failedGuid = new Guid("{CF1A8C1C-F2D0-41A1-A12C-53D9BE513A1C}");

            #region User repository mocks

            // User exists
            _userRepository
                .Setup(x => x.ExistsAsync(It.Is<string>(x => x == _successUser.Username), default))
                .ReturnsAsync(true);

            _userRepository
                .Setup(x => x.ExistsAsync(It.Is<string>(x => x == _failedUser.Username), default))
                .ReturnsAsync(false);

            // Add user
            _userRepository
                .Setup(x => x.AddAsync(It.Is<User>(x => x.Username == _successUser.Username && x.Password == _successUser.Password), default))
                .ReturnsAsync(successGuid);

            _userRepository
                .Setup(x => x.AddAsync(It.Is<User>(x => x.Username == _failedUser.Username && x.Password == _failedUser.Password), default))
                .ReturnsAsync(Guid.Empty);

            // Update user
            _userRepository
                .Setup(x => x.UpdateAsync(It.Is<User>(x => x.Username == _successUser.Username && x.Password == _successUser.Password), default))
                .ReturnsAsync(successGuid);

            _userRepository
                .Setup(x => x.UpdateAsync(It.Is<User>(x => x.Username == _failedUser.Username && x.Password == _failedUser.Password), default))
                .ReturnsAsync(Guid.Empty);

            // Delete user
            _userRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(x => x == successGuid), default))
                .ReturnsAsync(successGuid);

            _userRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(x => x == failedGuid), default))
                .ReturnsAsync(Guid.Empty);

            // Get user
            _userRepository
                .Setup(x => x.GetAsync(It.Is<Guid>(guid => guid == successGuid), default))
                .ReturnsAsync(_succeedDomainUser);

            _userRepository
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x == failedGuid), default))
                .ReturnsAsync((User)default!);

            _userRepository
                .Setup(x => x.GetAllAsync(default))
                .ReturnsAsync(_successUsers.AsQueryable());
            #endregion

            #region Add transient services
            AddServices();
            #endregion
        }

        private void AddServices()
        {
            // Add repository mocks
            _ = ServiceCollection.AddTransient(_ => _userRepository.Object);

            // Add handlers
            _ = ServiceCollection.AddTransient<CreateUserCommandHandler>();
            _ = ServiceCollection.AddTransient<UpdateUserCommandHandler>();
            _ = ServiceCollection.AddTransient<DeleteUserCommandHandler>();

            _ = ServiceCollection.AddTransient<GetUserQueryHandler>();
            _ = ServiceCollection.AddTransient<GetAllMoviesQueryHandler>();

            // Add validator
            _ = ServiceCollection.AddTransient<UserValidator<CreateUserCommand>>();
            _ = ServiceCollection.AddTransient<UserValidator<UpdateUserCommand>>();
        }

        private readonly BaseUserModel _successUser = new()
        {
            Username = "success",
            Password = "successpassword"
        };

        private readonly BaseUserModel _failedUser = new()
        {
            Username = "failed",
            Password = "failedpassword"
        };

        private static readonly User _succeedDomainUser = new()
        {
            Username = "succeeduser",
            Password = "succeedpassword"
        };

        private readonly List<User> _successUsers = new()
        {
            _succeedDomainUser,
            new User() { Username = "test", Password = "test" }
        };
    }
}
