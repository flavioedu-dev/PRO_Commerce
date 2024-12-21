using FluentAssertions;
using Moq;
using NUnit.Framework;
using PROCommerce.Authentication.API.Extentions.Mapper;
using PROCommerce.Authentication.Application.Services;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Enums;
using PROCommerce.Authentication.Domain.Exceptions;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using PROCommerce.Authentication.Domain.Interfaces.Services;
using System.Security;

namespace PROCommerce.Authentication.Application.Tests.Auth.Register;

[TestFixture]
public class RegisterServiceTests
{
    private RegisterDTO _registerDTOMock;
    private User _userMock;

    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ITokenServices> _tokenServiceMock;
    private Mock<IPasswordEncryption> _passwordEncryptionMock;

    [SetUp]
    public void Setup()
    {
        _registerDTOMock = new()
        {
            Username = "Jãozin",
            Password = "12345678",
            FullName = "Jão da Costa Silva",
            Email = "jaozinugostoso@gmail.com",
            Role = UserRole.Default
        };

        _userMock = new()
        {
            Id = 1
        };

        _unitOfWorkMock = new();
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUsername(It.IsAny<string>())).Returns<User>(null!);
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByEmail(It.IsAny<string>())).Returns<User>(null!);
        _unitOfWorkMock.Setup(x => x.UserRepository.Create(It.IsAny<User>()));
        _unitOfWorkMock.Setup(x => x.Commit());

        _tokenServiceMock = new();

        _passwordEncryptionMock = new();
        _passwordEncryptionMock.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(string.Empty);

        // Mapster
        MappingConfigurations.RegisterMaps(default!);
    }

    [TearDown]
    public void TearDown()
    {
        _registerDTOMock = default!;
        _unitOfWorkMock = default!;
        _tokenServiceMock = default!;
        _passwordEncryptionMock = default!;
    }

    [Test]
    public void Register_WhenDataIsValid_ThenDontThrowException()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Register(_registerDTOMock);

        // Assert
        act.Should().NotThrow();
    }

    [Test]
    public void Register_WhenUsernameExists_ThenThrowUsernameExistsException()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUsername(It.IsAny<string>())).Returns(_userMock);

        string errorMsg = "Nome de usuário indisponível";

        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Register(_registerDTOMock);

        // Assert
        act.Should().ThrowExactly<CustomResponseException>().WithMessage(errorMsg);
    }

    [Test]
    public void Register_WhenEmailExists_ThenThrowEmailExistsException()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.UserRepository.GetByEmail(It.IsAny<string>())).Returns(_userMock);

        string errorMsg = "Email já registrado";

        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        Action act = () => authServices.Register(_registerDTOMock);

        // Assert
        act.Should().ThrowExactly<CustomResponseException>().WithMessage(errorMsg);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenReturnRegisterResponseDTO()
    {
        // Act
        RegisterResponseDTO expectedRegisterResponseDTO = new()
        {
            Id = It.IsAny<long>(),
            Message = "Usuário cadastrado com sucesso"
        };

        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        registerResponseDTO.Should().BeEquivalentTo(expectedRegisterResponseDTO);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenGetByUsername()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.GetByUsername(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenGetByEmail()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.GetByEmail(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenHashPassword()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        _passwordEncryptionMock.Verify(x => x.HashPassword(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenCreate()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.UserRepository.Create(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public void Register_WhenDataIsValid_ThenCommit()
    {
        // Act
        AuthServices authServices = new(_unitOfWorkMock.Object, _tokenServiceMock.Object, _passwordEncryptionMock.Object);

        RegisterResponseDTO registerResponseDTO = authServices.Register(_registerDTOMock);

        // Assert
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }
}
