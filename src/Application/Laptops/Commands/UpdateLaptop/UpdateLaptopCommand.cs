using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Commands.UpdateLaptop;

public class UpdateLaptopCommand : IRequest<ApiResponse>
{
    public required Guid Id { get; init; }

    public required UpdateLaptopDTO LaptopDTO { get; init; }
}

public class UpdateLaptopCommandHandler : IRequestHandler<UpdateLaptopCommand, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    private readonly ILaptopsService _lsptopsService;

    public UpdateLaptopCommandHandler(IApplicationDbContext context, ILaptopsService lsptopsService)
    {
        _context = context;
        _lsptopsService = lsptopsService;

    }

    public async Task<ApiResponse> Handle(UpdateLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = await _lsptopsService.GetFirstByIdAsync(request.Id, cancellationToken);
        if (laptop is null)
        {
            return ResponseNotFound(request.Id);
        }
        laptop.Brand = request.LaptopDTO.Brand;
        laptop.Series = request.LaptopDTO.Series;
        laptop.Price = request.LaptopDTO.Price;
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            return ResponseInternalServerError(ex.Message);
        }
        return ResponseOk(laptop);
    }

    private static ApiResponse ResponseNotFound(Guid id)
    {
        return new ApiResponse(StatusCodes.Status404NotFound, new
        {
            Message = string.Format("There is no laptop with guid = {0,0}", id)
        });
    }

    private static ApiResponse ResponseInternalServerError(string message)
    {
        return new ApiResponse(StatusCodes.Status500InternalServerError, new
        {
            Message = message
        });
    }

    private static ApiResponse ResponseOk(Laptop laptop)
    {
        return new ApiResponse(StatusCodes.Status200OK, new
        {
            laptop
        });
    }
}
