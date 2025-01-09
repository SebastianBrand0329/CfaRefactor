namespace Cfa.Clientes.Domain.Models;

public class BaseResponseModel<T>
{
    public int StatusCode { get; set; }
    public bool Sucess { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}