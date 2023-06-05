using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Laptops.Commands.UpdateLaptop;

public class UpdateLaptopCommand : IRequest<IApiResponse>
{
    public required Guid Id { get; init; }

    public required UpdateLaptopDTO LaptopDTO { get; init; }
}

public class UpdateLaptopCommandHandler : IRequestHandler<UpdateLaptopCommand, IApiResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateLaptopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IApiResponse> Handle(UpdateLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop? laptop = _context.Laptops
            .Where(laptop => laptop.Id == request.Id)
            .FirstOrDefault();
        int entries = 0;
        if (laptop is not null)
        {
            UpdateLaptopDTO laptopDTO = request.LaptopDTO;
            laptop.Brand = laptopDTO.Brand;
            laptop.Series = laptopDTO.Series;
            laptop.Price = laptopDTO.Price;
            entries = await _context.SaveChangesAsync(cancellationToken);
        }
        return ResponseConverter.UpdateLaptopCommandResponse(laptop, request.Id, entries);
    }
}
