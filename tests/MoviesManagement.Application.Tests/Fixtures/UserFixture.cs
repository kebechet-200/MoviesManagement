using Microsoft.Extensions.DependencyInjection;
using Moq;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Users.Commands.Create;
using MoviesManagement.Application.Users.Commands.Delete;
using MoviesManagement.Application.Users.Commands.Update;
using MoviesManagement.Application.Users.Queries.Get;
using MoviesManagement.Application.Users.Queries.GetAll;

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
            
        }

    }
}
