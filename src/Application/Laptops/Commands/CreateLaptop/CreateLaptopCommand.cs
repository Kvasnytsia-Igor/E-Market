using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Laptops.Commands.CreateLaptop;

public class CreateLaptopCommand : IRequest<ApiResponse>
{
    public required string Brand { get; init; }

    public required string Series { get; init; }

    public required decimal Price { get; init; }
}

public class CreateLaptopCommandHandler : IRequestHandler<CreateLaptopCommand, ApiResponse>
{
    private readonly ILaptopsService _laptopsService;

    public CreateLaptopCommandHandler(ILaptopsService laptopsService)
    {
        _laptopsService = laptopsService;
    }

    public async Task<ApiResponse> Handle(CreateLaptopCommand request, CancellationToken cancellationToken)
    {
        Laptop laptop = new()
        {
            Brand = request.Brand,
            Price = request.Price,
            Series = request.Series,
        };
        int entries = await _laptopsService.Add(laptop, cancellationToken);
        Laptop? laptopFromDB = await _laptopsService.GetLastAdded(cancellationToken);
        return Response(laptopFromDB, entries);
    }

    private static ApiResponse Response(Laptop? laptopFromDB, int entries)
    {
        if (entries == 0)
        {
            return new ApiResponse(StatusCodes.Status500InternalServerError, new
            {
                Message = "The database changes is not secceded"
            });
        }
        else if (laptopFromDB is null)
        {
            return new ApiResponse(StatusCodes.Status500InternalServerError, new
            {
                Message = "Impossible to get the laptop from the database"
            });
        }
        else
        {
            return new ApiResponse(StatusCodes.Status201Created, new
            {
                laptopFromDB
            });
        }
    }
}