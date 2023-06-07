using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Laptops.Queries.GetLaptopById;

public class LaptopDTO : IMapFrom<Laptop>
{
    public required string Brand { get; set; }

    public required string Series { get; set; }

    public required decimal Price { get; set; }

    public required string Currency { get; set; }

    public required DateTime Created { get; set; }
}
