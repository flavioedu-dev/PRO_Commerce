using FluentAssertions;
using Moq;
using NUnit.Framework;
using PROCommerce.Authentication.API.Extentions.Mapper;
using PROCommerce.Authentication.Application.Services;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Exceptions;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using PROCommerce.Authentication.Domain.Interfaces.Services;

namespace PROCommerce.Authentication.Application.Tests.AuthServicesTests;

[TestFixture]
public class LoginTests
{
    private LoginDTO _loginDTOMock;
    private User _userMock;

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ITokenServices> _tokenServiceMock;
    private Mock<IPasswordEncryption> _passwordEncryptionMock;

    [SetUp]
    public void Setup()
    {
        _loginDTOMock = new()
        {
            Username = "Jãozin",
            Password = "12345678"
        };

        _userMock = new()
        {
            Id = 1
        };

        _unitOfWorkMock = new();
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUsername(It.IsAny<string>())).Returns(_userMock);

        _tokenServiceMock = new();
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(string.Empty);

        _passwordEncryptionMock = new();
        _passwordEncryptionMock.Setup(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Mapster
        MappingConfigurations.RegisterMaps(default!);
    }

    [TearDown]
    public void TearDown()
    {
        _loginDTOMock = default!;
        _unitOfWorkMock = default!;
        _tokenServiceMock = default!;
        _passwordEncryptionMock = default!;
    }

    [Test]
    public void Login_WhenDataIsValid_ThenDontThrowException()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Login(_loginDTOMock);

        // Assert
        act.Should().NotThrow();
    }

    [Test]
    public void Login_WhenUsernameNotExists_ThenThrowUserNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUsername(It.IsAny<string>())).Returns<User>(null!);

        string errorMsg = "Usuário não encontrado";

        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Login(_loginDTOMock);

        // Assert
        act.Should().ThrowExactly<CustomResponseException>().WithMessage(errorMsg);
    }

    [Test]
    public void Login_WhenPasswordIsIncorret_ThenThrowInvalidCrendentialsException()
    {
        // Arrange
        _passwordEncryptionMock.Setup(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        string errorMsg = "Credenciais inválidas";

        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Login(_loginDTOMock);

        // Assert
        act.Should().ThrowExactly<CustomResponseException>().WithMessage(errorMsg);
    }

    [Test]
    public void Login_WhenDataIsValid_ThenReturnLoginResponseDTO()
    {
        // Act
        LoginResponseDTO expectedLoginResponseDTO = new()
        {
            Id = _userMock.Id,
            Token = string.Empty
        };

        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        LoginResponseDTO loginResponseDTO = authServices.Login(_loginDTOMock);

        // Assert
        loginResponseDTO.Should().BeEquivalentTo(expectedLoginResponseDTO);
    }

    [Test]
    public void Login_WhenDataIsValid_ThenGetByUsername()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        LoginResponseDTO loginResponseDTO = authServices.Login(_loginDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.GetByUsername(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Login_WhenDataIsValid_ThenComparePassword()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        LoginResponseDTO loginResponseDTO = authServices.Login(_loginDTOMock);

        // Assert
        _passwordEncryptionMock.Verify(x => x.ComparePassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Login_WhenDataIsValid_ThenGenerateToken()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        LoginResponseDTO loginResponseDTO = authServices.Login(_loginDTOMock);

        // Assert
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);
    }
}
