using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Commands.CreateLaptop;

public class CreateLaptopCommand : IRequest<ApiResponse>
{
    public required string Brand { get; init; }

    public required string Series { get; init; }

    public required decimal Price { get; init; }
}

public class CreateLaptopCommandHandler : IRequestHandler<CreateLaptopCommand, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateLaptopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(CreateLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop laptop = new()
        {
            Brand = request.Brand,
            Price = request.Price,
            Series = request.Series,
        };
        _context.Laptops.Add(laptop);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            return ResponseInternalServerError(ex.Message);
        }
        return ResponseCreated(laptop);
    }

    private static ApiResponse ResponseInternalServerError(string message)
    {
        return new ApiResponse(StatusCodes.Status500InternalServerError, new
        {
            Message = message
        });
    }
    private static ApiResponse ResponseCreated(Laptop laptop)
    {
        return new ApiResponse(StatusCodes.Status201Created, new
        {
            laptop
        });
    }
}