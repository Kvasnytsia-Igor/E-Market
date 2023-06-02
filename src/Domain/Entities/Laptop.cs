using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Laptop : BaseAuditableEntity
{
    public required string Brand { get; set; }

    public required string Series { get; set; }

    public required decimal Price { get; set; }

    public Currency Currency { get; set; } = Currency.UAH;
}
