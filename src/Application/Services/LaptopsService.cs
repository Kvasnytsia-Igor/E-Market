using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Laptops.Queries.GetLaptopById;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class LaptopsService : ILaptopsService
{
    private readonly DbSet<Laptop> _laptopsRepository;

    private readonly IMapper _mapper;

    public LaptopsService(IApplicationDbContext context, IMapper mapper)
    {
        _laptopsRepository = context.Laptops;
        _mapper = mapper;
    }

    public async Task<Laptop?> GetFirstByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Laptop? laptop = await _laptopsRepository
          .Where(laptop => laptop.Id == id)
          .FirstOrDefaultAsync(cancellationToken);
        return laptop;
    }

    public async Task<LaptopDTO?> GetFirstDTOByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        LaptopDTO? laptopDTO = await _laptopsRepository
           .Where(laptop => laptop.Id == id)
           .ProjectTo<LaptopDTO>(_mapper.ConfigurationProvider)
           .AsNoTracking()
           .FirstOrDefaultAsync(cancellationToken);
        return laptopDTO;
    }

    public async Task<PaginatedList<Laptop>> GetPaginatedListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        PaginatedList<Laptop> paginatedList = await _laptopsRepository
            .OrderBy(l => l.Price)
            .PaginatedListAsync(pageNumber, pageSize);
        return paginatedList;
    }
}
