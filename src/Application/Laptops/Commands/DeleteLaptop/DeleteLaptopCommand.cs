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

    private readonly ILaptopsService _laptopsService;

    public DeleteLaptopCommandHandler(IApplicationDbContext context, ILaptopsService laptopsService)
    {
        _context = context;
        _laptopsService = laptopsService;
    }

    public async Task<ApiResponse> Handle(DeleteLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = await _laptopsService.GetFirstByIdAsync(request.Id, cancellationToken);
        if (laptop is null)
        {
            return ResponseNotFound(request.Id);
        }
        _context.Laptops.Remove(laptop);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            return ResponseInternalServerError(ex.Message);
        }
        return ResponseNoContent();
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

    private static ApiResponse ResponseNoContent()
    {
        return new ApiResponse(StatusCodes.Status204NoContent, new object());
    }
}