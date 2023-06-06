using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Queries.GetLaptopById;

public class GetLaptopByIdQuery : IRequest<ApiResponse>
{
    public required Guid Id { get; init; }
}

public class GetLaptopByIdQueryHandler : IRequestHandler<GetLaptopByIdQuery, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetLaptopByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(GetLaptopByIdQuery request, CancellationToken cancellationToken)
    {
        Laptop? laptop = await _context.Laptops
            .Where(laptop => laptop.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return Response(laptop, request.Id);
    }

    private static ApiResponse Response(Laptop? laptop, Guid guid)
    {
        if (laptop == null)
        {
            return new ApiResponse(StatusCodes.Status404NotFound, new
            {
                Message = string.Format("There is no laptop with guid = {0,0}", guid)
            });
        }
        else
        {
            return new ApiResponse(StatusCodes.Status200OK, new
            {
                laptop
            });
        }
    }
}
