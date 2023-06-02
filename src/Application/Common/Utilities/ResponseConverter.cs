using Domain.Entities;

namespace Application.Common.Models;

public class ResponseConverter
{
    private const string EMPTY_LIST_MESSAGE = "The list with laptops is empty";

    public static IApiResponse GetLaptopsWithPaginationResponse(PaginatedList<Laptop> paginatedList)
    {
        if (paginatedList.Items.Any())
        {
            return new ApiResponse200(paginatedList);
        }
        return new ApiResponse404(EMPTY_LIST_MESSAGE);
    }

    private const string NOT_FOUND_OBJECT = "There is no laptop with guid = {0,0}";

    public static IApiResponse GetLaptopByIdResponse(Laptop? laptop, Guid guid)
    {
        if (laptop != null)
        {
            return new ApiResponse200(laptop);
        }
        return new ApiResponse404(string.Format(NOT_FOUND_OBJECT, guid));
    }

    public static IApiResponse DeleteLaptopResponse(Laptop? laptop, Guid guid)
    {
        if (laptop != null)
        {
            return new ApiResponse204();
        }
        return new ApiResponse404(string.Format(NOT_FOUND_OBJECT, guid));
    }

    private const string NOT_SAVED = "The object is not saved";

    public static IApiResponse CreateLaptopResponse(Laptop? laptop)
    {
        if (laptop == null)
        {
            return new ApiResponse500(NOT_SAVED);
        }
        return new ApiResponse201(laptop);
    }

    public static IApiResponse UpdateLaptopResponse(Laptop? laptop, Guid id, int entries)
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


}
