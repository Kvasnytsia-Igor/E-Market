using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class LaptopsService : ILaptopsService
{
    private readonly IApplicationDbContext _context;

    private readonly DbSet<Laptop> _leptopsRepository;

    public LaptopsService(IApplicationDbContext context)
    {
        _context = context;
        _leptopsRepository = context.Laptops;
    }

    public async Task<int> Add(Laptop laptop, CancellationToken cancellationToken)
    {
        _leptopsRepository.Add(laptop);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Laptop?> GetLastAdded(CancellationToken cancellationToken)
    {
        Laptop? laptopFromDB = await _leptopsRepository
           .OrderByDescending(user => user.Created)
           .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return laptopFromDB;
    }
}
