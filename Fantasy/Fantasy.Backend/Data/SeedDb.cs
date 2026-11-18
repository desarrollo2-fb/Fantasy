using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
        await CheckTeamsAsync();
    }

    private async Task CheckCountriesAsync()
    {
        if (!await _context.Countries.AnyAsync())
        {
            var countriesSQLScript = await File.ReadAllTextAsync("Data\\Countries.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckTeamsAsync()
    {
        if (!await _context.Teams.AnyAsync())
        {
            var countries = await _context.Countries.ToListAsync();
            foreach (var country in countries)
            {
                _context.Teams.Add(new Team { Name = country.Name, Country = country });
                if (country.Name == "Argentina")
                {
                    _context.Teams.Add(new Team { Name = "River Plate", Country = country });
                    _context.Teams.Add(new Team { Name = "Boca Juniors", Country = country });
                    _context.Teams.Add(new Team { Name = "Independiente", Country = country });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}