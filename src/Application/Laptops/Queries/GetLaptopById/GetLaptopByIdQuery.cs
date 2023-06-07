using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly DbSet<Laptop> _laptopsRepository;

    private readonly IMapper _mapper;

    public GetLaptopByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _laptopsRepository = context.Laptops;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(GetLaptopByIdQuery request, CancellationToken cancellationToken)
    {
        LaptopDTO? laptopDTO = await _laptopsRepository
            .Where(laptop => laptop.Id == request.Id)
            .ProjectTo<LaptopDTO>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
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
