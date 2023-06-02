using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Laptops.Commands.CreateLaptop;

public class CreateLaptopCommand : IRequest<IApiResponse>
{
    public required string Brand { get; init; }

    public required string Series { get; init; }

    public required decimal Price { get; init; }
}

public class CreateLaptopCommandHandler : IRequestHandler<CreateLaptopCommand, IApiResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateLaptopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResponse> Handle(CreateLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = new()
        {
            Brand = request.Brand,
            Price = request.Price,
            Series = request.Series,
        };
        _context.Laptops.Add(laptop);
        await _context.SaveChangesAsync(cancellationToken);
        Laptop? fromDB = _context.Laptops
            .OrderByDescending(user => user.Id)
            .FirstOrDefault();
        return ResponseConverter.CreateLaptopResponse(fromDB);
    }
}