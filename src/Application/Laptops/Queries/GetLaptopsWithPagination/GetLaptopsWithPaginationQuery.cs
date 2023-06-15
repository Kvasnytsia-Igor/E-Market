using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Laptops.Queries.GetLaptopsWithPagination;

public record class GetLaptopsWithPaginationQuery : IRequest<ApiResponse>
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }
}

public class GetLaptopsWithPaginationQueryHeandler : IRequestHandler<GetLaptopsWithPaginationQuery, ApiResponse>
{
    private readonly ILaptopsService _laptopsService;

    public GetLaptopsWithPaginationQueryHeandler(ILaptopsService laptopsService)
    {
        _laptopsService = laptopsService;
    }

    public async Task<ApiResponse> Handle(GetLaptopsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<Laptop> paginatedList = await _laptopsService
            .GetPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        if (!paginatedList.Items.Any())
        {
            return ResponseNotFound();
        }
        return ResponseOK(paginatedList);
    }

    private static ApiResponse ResponseNotFound()
    {
        return new ApiResponse(StatusCodes.Status404NotFound, new
        {
            Message = "The list with laptops is empty"
        });
    }

    private static ApiResponse ResponseOK(PaginatedList<Laptop> paginatedList)
    {
        return new ApiResponse(StatusCodes.Status200OK, new
        {
            paginatedList
        });
    }
}