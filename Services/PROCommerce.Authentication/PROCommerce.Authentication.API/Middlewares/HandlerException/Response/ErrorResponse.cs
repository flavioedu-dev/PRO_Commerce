using System.Text.Json.Serialization;

namespace PROCommerce.Authentication.API.Middlewares.HandlerException.Response;

public class ErrorResponse
{
    [JsonPropertyName("sucess")]
    public bool Sucess { get; set; } = false;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}