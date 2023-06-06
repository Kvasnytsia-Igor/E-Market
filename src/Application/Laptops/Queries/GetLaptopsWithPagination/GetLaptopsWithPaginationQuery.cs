using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Queries.GetLaptopsWithPagination;

public record class GetLaptopsWithPaginationQuery : IRequest<ApiResponse>
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }
}

public class GetLaptopsWithPaginationQueryHeandler : IRequestHandler<GetLaptopsWithPaginationQuery, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetLaptopsWithPaginationQueryHeandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(GetLaptopsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Laptop> laptops = _context.Laptops.AsNoTracking();
        PaginatedList<Laptop> paginatedList = await PaginatedList<Laptop>
            .CreateAsync(laptops, request.PageNumber, request.PageSize);
        return Response(paginatedList);
    }

    private static ApiResponse Response(PaginatedList<Laptop> paginatedList)
    {
        if (!paginatedList.Items.Any())
        {
            return new ApiResponse(StatusCodes.Status404NotFound, new
            {
                Message = "The list with laptops is empty"
            });
        }
        else
        {
            return new ApiResponse(StatusCodes.Status200OK, new
            {
                paginatedList
            });
        }
    }
}