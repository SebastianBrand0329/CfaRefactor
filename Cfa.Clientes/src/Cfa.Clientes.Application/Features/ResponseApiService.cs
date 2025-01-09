using Cfa.Clientes.Domain.Models;

namespace Cfa.Clientes.Application.Features;

public static class ResponseApiService<T>
{
    public static BaseResponseModel<T> Response(int statusCode, object Data = null, string message = null)
    {
        bool sucess = false;

        if (statusCode >= 200 && statusCode < 300)
            sucess = true;

        var result = new BaseResponseModel<T>
        {
            StatusCode = statusCode,
            Sucess = sucess,
            Message = message,
            Data = (T)Data
        };

        return result;
    }
}