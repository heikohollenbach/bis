namespace BIS.Common.Exceptions;

/// <summary>
/// Base exception for game-related errors
/// </summary>
public class BISException : Exception
{
    public string Code { get; set; }

    public BISException(string message, string code = "GAME_ERROR") : base(message)
    {
        Code = code;
    }

    public BISException(string message, string code, Exception innerException) 
        : base(message, innerException)
    {
        Code = code;
    }
}

/// <summary>
/// Exception for authentication-related errors
/// </summary>
public class AuthenticationException : BISException
{
    public AuthenticationException(string message) : base(message, "AUTH_ERROR") { }
}

/// <summary>
/// Exception for validation errors
/// </summary>
public class ValidationException : BISException
{
    public ValidationException(string message) : base(message, "VALIDATION_ERROR") { }
}

/// <summary>
/// Exception for resource not found errors
/// </summary>
public class NotFoundException : BISException
{
    public NotFoundException(string message) : base(message, "NOT_FOUND") { }
}
