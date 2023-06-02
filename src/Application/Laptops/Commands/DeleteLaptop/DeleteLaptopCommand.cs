using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Laptops.Commands.DeleteLaptop;

public class DeleteLaptopCommand : IRequest<IApiResponse>
{
    public required Guid Id { get; init; }
}

public class DeleteLaptopCommandHandler : IRequestHandler<DeleteLaptopCommand, IApiResponse>
{

    private readonly IApplicationDbContext _context;

    public DeleteLaptopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResponse> Handle(DeleteLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = _context.Laptops
           .Where(laptop => laptop.Id == request.Id)
           .FirstOrDefault();
        if (laptop is not null)
        {
            _context.Laptops.Remove(laptop);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return ResponseConverter.DeleteLaptopResponse(laptop, request.Id);
    }
}