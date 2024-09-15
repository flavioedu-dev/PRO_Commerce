using System.ComponentModel;

namespace PROCommerce.Authentication.Domain.Enums;

public enum UserRole
{
    [Description("Usuário padrão.")]
    Default = 0,
    [Description("Administrador")]
    Admin = 1
}