namespace PROCommerce.Authentication.API.Middlewares.HandlerException.Exceptions;

public class CustomResponseException : Exception
{
    internal int? StatusCode {  get; set; }


    public CustomResponseException ()
    {
    }

    public CustomResponseException (string message)
        : base (message)
    {

    }

    public CustomResponseException (string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}