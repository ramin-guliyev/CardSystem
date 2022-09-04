namespace WebUI.Helpers.Exceptions;

public record ApiException(int statusCode, string message = null, string details = null);
