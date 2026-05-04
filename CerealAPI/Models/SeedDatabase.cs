using Cereal_API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cereal_API.Models;

public static class SeedDatabase
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CerealDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CerealDbContext>>()))
        {
            // Look for any cereals.
            if (context.Cereals.Any())
            {
                return;   // DB has been seeded
            }
            context.Cereals.AddRange(CsvToDataParser.CsvDataToCereals("../CerealAPI/cereal.csv"));
            context.SaveChanges();
        }
    }
}
