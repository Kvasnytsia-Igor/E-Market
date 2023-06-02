using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Queries.GetLaptopsWithPagination;

public record class GetLaptopsWithPaginationQuery : IRequest<IApiResponse>
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }
}

public class GetLaptopsWithPaginationQueryHeandler : IRequestHandler<GetLaptopsWithPaginationQuery, IApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetLaptopsWithPaginationQueryHeandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResponse> Handle(GetLaptopsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Laptop> laptops = _context.Laptops.AsNoTracking();
        PaginatedList<Laptop> paginatedList = await PaginatedList<Laptop>
            .CreateAsync(laptops, request.PageNumber, request.PageSize);
        return ResponseConverter.GetLaptopsWithPaginationResponse(paginatedList);
    }
}