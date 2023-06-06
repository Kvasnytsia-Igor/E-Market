using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ILaptopsService
{
    Task<int> Add(Laptop laptop, CancellationToken cancellationToken);

    Task<Laptop?> GetLastAdded(CancellationToken cancellationToken);
}
