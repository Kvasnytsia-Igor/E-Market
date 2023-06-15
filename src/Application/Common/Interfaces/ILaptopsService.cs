using Application.Common.Models;
using Application.Laptops.Queries.GetLaptopById;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ILaptopsService
{
    Task<Laptop?> GetFirstByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<LaptopDTO?> GetFirstDTOByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedList<Laptop>> GetPaginatedListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
