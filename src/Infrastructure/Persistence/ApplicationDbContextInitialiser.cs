using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Laptops.Any())
        {
            _context.Laptops.AddRange(new Laptop[]
            {
                new Laptop
                {
                    Brand = "Acer",
                    Series = "Aspire 7",
                    Price = 29999m,
                    Created = DateTime.Now,
                },
                new Laptop
                {
                    Brand = "ASUS",
                    Series = "Laptop",
                    Price = 18599m,
                    Created = DateTime.Now,
                },
                new Laptop
                {
                    Brand = "Lenovo",
                    Series = "IdeaPad 3",
                    Price = 23999m,
                    Created = DateTime.Now,
                }
            });
            await _context.SaveChangesAsync();
        }
    }
}
