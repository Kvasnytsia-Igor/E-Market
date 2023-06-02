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
            List<Laptop> laptops = new();
            for (int i = 0; i < 20; i++)
            {
                laptops.Add(new Laptop
                {
                    Brand = $"Brand{i}",
                    Price = Random.Shared.Next(1, 25),
                    Series = $"Series{i}",
                });
            }
            _context.Laptops.AddRange(laptops);
            await _context.SaveChangesAsync();
        }
    }
}
