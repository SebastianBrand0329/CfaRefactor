using Cfa.Clientes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Cfa.Clientes.Application.Helpers.Logs;

public static class ExtensionMethods
{
    public static BaseResponseModel<T> Warn<T, R>(this BaseResponseModel<T> response, object request = null, BaseResponseModel<R> result = null)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        response.Message = result?.Message;

        DatabasePath();
        LoggerManager.logger.WithProperty("inputParams", request).Error(response.Message);

        return response;
    }

    public static BaseResponseModel<T> Warn<T>(this BaseResponseModel<T> response, object request = null)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        DatabasePath();
        LoggerManager.logger.WithProperty("inputParams", request).Warn(response.Message);

        return response;
    }

    public static BaseResponseModel<T> Warn<T>(this BaseResponseModel<T> response, Exception ex, object request = null)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        response.Message = $"{ex.Message} {ex.InnerException}";

        DatabasePath();
        LoggerManager.logger.WithProperty("inputParams", request).Warn(response.Message);

        return response;
    }

    public static BaseResponseModel<T> Error<T>(this BaseResponseModel<T> response, ResultException ex, object request = null)
    {
        response.Message = $"{ex.Detail} {ex.Message}";
        response.StatusCode = ex.Code;

        DatabasePath();
        LoggerManager.logger.WithProperty("inputParams", request)
            .Error(@$"{ex.Message}. Detail:{ex.Detail}. StackTrace: {ex.StackTrace} Source: {ex.Source}");

        return response;
    }

    public static BaseResponseModel<T> Exception<T>(this BaseResponseModel<T> response, Exception ex, object request = null)
    {
        response.Message = $"{ex.Message} {ex.InnerException}";

        DatabasePath();
        LoggerManager.logger.WithProperty("inputParams", request)
            .Error(@$"{ex.Message} {ex.InnerException} StackTrace: {ex.StackTrace} Source: {ex.Source}");

        return response;
    }

    public static BaseResponseModel<T> Success<T>(this BaseResponseModel<T> response, object request = null, BaseResponseModel<T> result = null)
    {
        if (response is null)
            throw new ArgumentNullException(nameof(response));

        response.Data = result != null ? result.Data : response.Data != null ? response.Data : default;
        //response.StatusCode = AppSettings.Settings.ResponseResult;
        response.Message = "OK";
        //response.HttpStatusCode = HttpStatusCode.OK;
        response.Sucess = true;

        var method = new StackTrace().GetFrame(1).GetMethod().GetRealMethodFromAsyncMethod()?.Name?.ToLower();

        var segment = AppSettings.Settings?.Logging?.Segments?.Where(s => s.ToLower().Equals(method))?.SingleOrDefault();
        if (segment != null)
        {
            DatabasePath();
            LoggerManager.logger.WithProperty("inputParams", request).WithProperty("response", response)
            .Info(response.Message);
        }
        return response;
    }

    public static ResponseProblem Bad<T>(this ResponseProblem error, BaseResponseModel<T> response)
    {
        error.StatusMessage = response.Message;
        error.StatusCode = response.StatusCode;
        return error;
    }

    public static ResponseProblem Bad(this ResponseProblem error)
    {
        DatabasePath();
        LoggerManager.logger.WithProperty("response", error.Title).Error(error.StatusMessage);
        return error;
    }

    //public static ResponseProblem Error(this ResponseProblem error, ValidationResult response)
    //{
    //    var errors = string.Join('|', response.Errors?.Select(v => v.ErrorMessage));

    //    error.Title = "One or more validation errors occurred";
    //    error.StatusCode = AppSettings.Settings.ErrorResult;
    //    error.StatusMessage = errors;
    //    return error;
    //}

    public static MethodBase GetRealMethodFromAsyncMethod(this MethodBase asyncMethod)
    {
        var generatedType = asyncMethod.DeclaringType;
        var originalType = generatedType.DeclaringType;
        if (originalType != null)
        {
            var matchingMethods =
                from methodInfo in originalType.GetMethods()
                let attr = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>()
                where attr != null && attr.StateMachineType == generatedType
                select methodInfo;

            // If this throws, the async method scanning failed.
            var foundMethod = matchingMethods.Single();
            return foundMethod;
        }
        else
            return asyncMethod;
    }


    private static void DatabasePath()
    {
        if (!string.IsNullOrEmpty(AppSettings.Settings.Logging.DbName))
        {
            var date = DateTime.Now;
            var dateFolder = @$"{date:yyyy}\{date.ToString("MMMM", CultureInfo.InvariantCulture)}\{date:dd}";
            string sourceDatabase = @$"{AppSettings.Settings.Logging.DefaultConnection}\{dateFolder}\{AppSettings.Settings.Logging.FolderDatabase}";
            if (!Directory.Exists(sourceDatabase))
                Directory.CreateDirectory(sourceDatabase);

            string fileCompact = @$"{sourceDatabase}\{AppSettings.Settings.Logging.DbName}";
            if (!File.Exists(fileCompact))
            {
                var connectionString = $@"Data Source={fileCompact}";
                var optionsBuilder = new DbContextOptionsBuilder<LogDbContext>();
                optionsBuilder.UseSqlite(connectionString);
                using var context = new LogDbContext(optionsBuilder.Options);
                context.Database.EnsureCreated();
            }
        }
    }

}
