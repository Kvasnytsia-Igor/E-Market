using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Queries.GetLaptopById;

public class GetLaptopByIdQuery : IRequest<IApiResponse>
{
    public required Guid Id { get; init; }
}

public class GetLaptopByIdQueryHandler : IRequestHandler<GetLaptopByIdQuery, IApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetLaptopByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResponse> Handle(GetLaptopByIdQuery request, CancellationToken cancellationToken)
    {
        Laptop? laptop = await _context.Laptops
            .Where(laptop => laptop.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return ResponseConverter.GetLaptopByIdResponse(laptop, request.Id);
    }
}
