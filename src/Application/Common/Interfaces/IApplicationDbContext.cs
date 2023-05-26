using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Laptop> Laptops { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
