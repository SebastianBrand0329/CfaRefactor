using System.Runtime.Serialization;

namespace Cfa.Clientes.Domain.Models;

[Serializable]
public class ResultException : Exception
{
    private static readonly string DefaultMessage = ".";

    public int Code { get; set; }
    public string Detail { get; set; }

    public ResultException() : base(DefaultMessage)
    {
    }

    public ResultException(string message) : base(message)
    {
    }

    public ResultException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ResultException(int code, string detail) : base(DefaultMessage)
    {
        Code = code;
        Detail = detail;
    }

    public ResultException(int code, string detail, Exception innerException) : base(DefaultMessage, innerException)
    {
        Code = code;
        Detail = detail;
    }

    protected ResultException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
