using System.Text.Json.Serialization;

namespace PROCommerce.Authentication.API.Middlewares.HandlerException.Response;

public class ErrorResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; } = false;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}