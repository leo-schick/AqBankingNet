namespace AqBanking;

public class AqBankingException : Exception
{
    public AqBankingException(int errorCode)
        : base($"AqBanking error code {errorCode}")
    {
        ErrorCode = errorCode;

        // TODO add mapping of error code to proper user message
    }

    public AqBankingException(int errorCode, string message)
        : base(string.Format(message, errorCode))
    {
        ErrorCode = errorCode;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int ErrorCode { get; }
}

public static class ErrorHandling
{
    /// <summary>
    /// Checks the return value of a function for a error:
    /// If return value is zero, all is fine. Otherwise an error happened and is thrown via
    /// exception class <see also="AqBankingException"/>.
    /// </summary>
    public static void CheckForErrors(int returnValue)
    {
        if (returnValue != 0)
            throw new AqBankingException(returnValue);
    }

    public static void CheckForErrors(int returnValue, string errorMessage)
    {
        if (returnValue != 0)
            throw new AqBankingException(returnValue, errorMessage);
    }
}