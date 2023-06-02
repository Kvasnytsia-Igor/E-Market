namespace Application.Laptops.Commands.UpdateLaptop;

public class UpdateLaptopDTO
{
    public required string Brand { get; init; }

    public required string Series { get; init; }

    public required decimal Price { get; init; }
}
