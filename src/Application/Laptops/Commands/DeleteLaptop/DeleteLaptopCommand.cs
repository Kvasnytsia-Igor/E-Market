using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Laptops.Commands.DeleteLaptop;

public class DeleteLaptopCommand : IRequest<ApiResponse>
{
    public required Guid Id { get; init; }
}

public class DeleteLaptopCommandHandler : IRequestHandler<DeleteLaptopCommand, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public DeleteLaptopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(DeleteLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = await _context.Laptops
           .Where(laptop => laptop.Id == request.Id)
           .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        int entries = 0;
        if (laptop is not null)
        {
            _context.Laptops.Remove(laptop);
            entries = await _context.SaveChangesAsync(cancellationToken);
        }
        return Response(laptop, request.Id, entries);
    }

    private static ApiResponse Response(Laptop? laptop, Guid id, int entries)
    {
        if (laptop == null)
        {
            return new ApiResponse(StatusCodes.Status404NotFound, new
            {
                Message = string.Format("There is no laptop with guid = {0,0}", id)
            });
        }
        else if (entries == 0)
        {
            return new ApiResponse(StatusCodes.Status500InternalServerError, new
            {
                Message = "The database changes is not secceded"
            });
        }
        else
        {
            return new ApiResponse(StatusCodes.Status204NoContent, new object());
        }
    }
}