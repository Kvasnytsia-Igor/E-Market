using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Laptop : BaseAuditableEntity
{
    public string Brand { get; set; } = string.Empty;

    public string Series { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Currency Currency { get; set; } = Currency.UAH;
}
