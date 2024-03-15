namespace TrackYourSpendings.Web.Exceptions;

public class SendFailException : Exception
{
    public SendFailException()
    {
    }

    public SendFailException(string message) : base(message)
    {
    }

    public SendFailException(string message, Exception innerException) : base(message, innerException)
    {
    }
}