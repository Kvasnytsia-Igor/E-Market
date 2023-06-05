using Domain.Entities;

namespace Application.Common.Models;

public class ResponseConverter
{
    private const string EMPTY_LIST_MESSAGE = "The list with laptops is empty";

    private const string NOT_FOUND_OBJECT = "There is no laptop with guid = {0,0}";

    private const string NOT_SAVED = "The database changes is not secceded";

    public static IApiResponse GetLaptopsWithPaginationResponse(PaginatedList<Laptop> paginatedList)
    {
        if (paginatedList.Items.Any())
        {
            return new ApiResponse200(paginatedList);
        }
        return new ApiResponse404(EMPTY_LIST_MESSAGE);
    }

    public static IApiResponse GetLaptopByIdResponse(Laptop? laptop, Guid guid)
    {
        if (laptop == null)
        {
            return new ApiResponse404(string.Format(NOT_FOUND_OBJECT, guid));
        }
        return new ApiResponse200(laptop);
    }

    public static IApiResponse CreateLaptopCommandResponse(Laptop? laptop, int entries)
    {
        if (entries == 0 || laptop is null)
        {
            return new ApiResponse500(NOT_SAVED);
        }
        return new ApiResponse201(laptop);
    }

    public static IApiResponse UpdateLaptopCommandResponse(Laptop? laptop, Guid id, int entries)
    {
        if (laptop == null)
        {
            return new ApiResponse404(string.Format(NOT_FOUND_OBJECT, id));
        }
        else if (entries == 0)
        {
            return new ApiResponse500(NOT_SAVED);
        }
        return new ApiResponse200(laptop);
    }

    public static IApiResponse DeleteLaptopCommandResponse(Laptop? laptop, Guid id, int entries)
    {
        if (laptop == null)
        {
            return new ApiResponse404(string.Format(NOT_FOUND_OBJECT, id));
        }
        else if (entries == 0)
        {
            return new ApiResponse500(NOT_SAVED);
        }
        return new ApiResponse204();
    }
}
