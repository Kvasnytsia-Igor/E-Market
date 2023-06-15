using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Laptops.Queries.GetLaptopById;

public class GetLaptopByIdQuery : IRequest<ApiResponse>
{
    public required Guid Id { get; init; }
}

public class GetLaptopByIdQueryHandler : IRequestHandler<GetLaptopByIdQuery, ApiResponse>
{
    private readonly ILaptopsService _laptopsService;

    public GetLaptopByIdQueryHandler(ILaptopsService laptopsService)
    {
        _laptopsService = laptopsService;
    }

    public async Task<ApiResponse> Handle(GetLaptopByIdQuery request, CancellationToken cancellationToken)
    {
        LaptopDTO? laptopDTO = await _laptopsService.GetFirstDTOByIdAsync(request.Id, cancellationToken);
        if (laptopDTO is null)
        {
            return NotFoundResponse(request.Id);
        }
        return OkResponse(laptopDTO);
    }

    private static ApiResponse OkResponse(LaptopDTO dto)
    {
        return new ApiResponse(StatusCodes.Status200OK, new
        {
            Result = dto
        });
    }

    private static ApiResponse NotFoundResponse(Guid guid)
    {
        return new ApiResponse(StatusCodes.Status404NotFound, new
        {
            Message = string.Format("There is no laptop with guid = {0,0}", guid)
        });
    }
}
