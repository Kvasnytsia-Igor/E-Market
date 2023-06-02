using Microsoft.AspNetCore.Http;

namespace Application.Common.Models;


public interface IApiResponse
{
    object Result { get; }

    int StatusCode { get; }
}


public class ApiResponse200 : IApiResponse
{
    private readonly object _data;

    public ApiResponse200(object data)
    {
        _data = data;
    }

    public object Result
    {
        get
        {
            return _data;
        }
    }

    public int StatusCode => StatusCodes.Status200OK;
}

public class ApiResponse201 : ApiResponse200, IApiResponse
{
    public ApiResponse201(object data) : base(data) { }

    public new int StatusCode => StatusCodes.Status201Created;
}

public class ApiResponse204 : IApiResponse
{
    public int StatusCode => StatusCodes.Status204NoContent;

    public object Result => new();
}

public class ApiResponse400 : IApiResponse
{
    public readonly IList<string> _errors;

    public ApiResponse400(IList<string> errors)
    {
        _errors = errors;
    }

    public object Result
    {
        get
        {
            return new
            {
                ValidationErrors = _errors
            };
        }
    }

    public int StatusCode => StatusCodes.Status400BadRequest;
}

public class ApiResponse404 : IApiResponse
{
    private readonly string _message;

    public ApiResponse404(string message)
    {
        _message = message;
    }

    public object Result
    {
        get
        {
            return new
            {
                Message = _message
            };
        }
    }

    public int StatusCode => StatusCodes.Status404NotFound;
}

public class ApiResponse500 : IApiResponse
{
    private readonly string _message;

    public ApiResponse500(string message)
    {
        _message = message;
    }

    public object Result
    {
        get
        {
            return new
            {
                Message = _message
            };
        }
    }

    public int StatusCode => StatusCodes.Status500InternalServerError;
}
