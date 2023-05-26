using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Queries.GetLaptopsWithPagination;

public record class GetLaptopsWithPaginationQuery : IRequest<PaginatedList<Laptop>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetLaptopsWithPaginationQueryHeandler : IRequestHandler<GetLaptopsWithPaginationQuery, PaginatedList<Laptop>>
{
    private readonly IApplicationDbContext _context;

    public GetLaptopsWithPaginationQueryHeandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Laptop>> Handle(GetLaptopsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Laptop> laptops = _context.Laptops.AsNoTracking();
        return await PaginatedList<Laptop>.CreateAsync(laptops, request.PageNumber, request.PageSize);
    }
}