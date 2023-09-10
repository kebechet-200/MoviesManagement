using FluentAssertions;
using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Application.Users.Commands.Create;
using MoviesManagement.Domain.Common.Exceptions;
using Xunit;
using Xunit.Sdk;

namespace MoviesManagement.Application.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : IClassFixture<UserFixture>
    {
        private readonly UserFixture _userFixture;

        public CreateUserCommandHandlerTests(UserFixture userFixture)
        {
            _userFixture = userFixture;
        }

        [Fact]
        public async Task Handle_WhenCancellationIsRequested_ReturnError()
        {
            // Arrange
            CancellationTokenSource source = new();
            var cancellationToken = source.Token;
            Exception exception = default!;
            var handler = _userFixture.CreateUserCommandHandler;

            // Act
            try
            {
                source.Cancel();
                _ = await handler.Handle(_emptyUsername, cancellationToken);
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<OperationCanceledException>();
            exception.Message.Should().Be("Operation cancelled");
        }

        [Theory]
        [MemberData(nameof(EmptyUserData))]
        public async Task Handle_WhenParametersAreEmpty_ShouldReturnError(Type expectedExceptionType, string expectedMessage, CreateUserCommand command)
        {
            //Arrange
            var handler = _userFixture.CreateUserCommandHandler;
            Exception exception = default!;

            //Act
            try
            {
                _ = await handler.Handle(command, CancellationToken.None);
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.GetType().Should().NotBeNull().And.Be(expectedExceptionType);
            exception.Message.Should().Be(expectedMessage);
        }

        [Theory]
        [MemberData(nameof(NotFitInLength))]
        public async Task Handle_WhenParametersAreMoreOrLess_ShouldReturnError(Type expectedExceptionType, string expectedMessage, CreateUserCommand command)
        {
            //Arrange
            var handler = _userFixture.CreateUserCommandHandler;
            Exception exception = default!;

            //Act
            try
            {
                _ = await handler.Handle(command, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.GetType().Should().NotBeNull().And.Be(expectedExceptionType);
            exception.Message.Should().Be(expectedMessage);
        }

        [Fact]
        public async Task Handle_WhenUserAlreadyExistInDatabase_ShouldThrowException()
        {
            var _mediator = _userFixture.CreateUserCommandHandler;
            Exception exception = default!;
            
            try
            {
                _ = await _mediator.Handle(_userAlreadyExists, CancellationToken.None);
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType<UserAlreadyExistsException>();
            exception.Message.Should().Be($"User with name {_userAlreadyExists.Username} already exsits");
        }

        [Fact]
        public async Task Handle_WhenUserCannotBeAdded_ShouldThrowException()
        {
            var _mediator = _userFixture.CreateUserCommandHandler;
            Exception exception = default!;

            try
            {
                _ = await _mediator.Handle(_failWhileAdd, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType< UserNotRegisteredException>();
            exception.Message.Should().Be($"There was some problem while adding the user, try again later");
        }

        [Fact]
        public async Task Handle_WhenEverythingsCorrect_ShouldAddTheUser()
        {
            var _mediator = _userFixture.CreateUserCommandHandler;

            Exception exception = default!;
            Unit result;

            try
            {
                result = await _mediator.Handle(_everythingSucceedCommand, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
            result.Should().Be(Unit.Value).And.BeOfType(typeof(Unit));
        }

        #region TestData
        public static IEnumerable<object[]> EmptyUserData()
        {
            yield return new object[] { typeof(UserValidationException), ErrorMessages.UsernameShouldNotBeEmpty, _emptyUsername };
            yield return new object[] { typeof(UserValidationException), ErrorMessages.PasswordShouldNotBeEmpty, _emptyPassword };
        }
        public static IEnumerable<object[]> NotFitInLength()
        {
            yield return new object[] { typeof(UserValidationException), ErrorMessages.LessOrMoreUsernameLength, _lessUsername };
            yield return new object[] { typeof(UserValidationException), ErrorMessages.LessOrMoreUsernameLength, _moreUsername };
            yield return new object[] { typeof(UserValidationException), ErrorMessages.LessOrMorePasswordLength, _lessPassword };
            yield return new object[] { typeof(UserValidationException), ErrorMessages.LessOrMorePasswordLength, _morePassword };
        }
        private static CreateUserCommand _emptyUsername = new() { Username = string.Empty, Password = "11111111"};
        private static CreateUserCommand _emptyPassword = new() { Username = "11111111", Password = string.Empty};
        private static CreateUserCommand _lessPassword = new() { Username = "11111111", Password = "111"};
        private static CreateUserCommand _morePassword = new() { Username = "11111111", Password = "1111111111111111" };
        private static CreateUserCommand _lessUsername = new() { Username = "111", Password = "11111111" };
        private static CreateUserCommand _moreUsername = new() { Username = "1111111111111111", Password = "11111111" };
        private static CreateUserCommand _everythingSucceedCommand = new() { Username = "succeeduser", Password = "successpassword" };
        private static CreateUserCommand _failWhileAdd = new() { Username = "faileduser", Password = "failedpassword" };
        private static CreateUserCommand _userAlreadyExists = new() { Username = "failed exist", Password = "11111111" };
        #endregion
    }   
}
