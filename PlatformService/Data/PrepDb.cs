using Microsoft.IdentityModel.Tokens;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PreparateDb(WebApplicationBuilder app)
        {
            using (var serviceScoped = app.Services.BuildServiceProvider().CreateScope())
            {
                SeedData(serviceScoped.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");

                dbContext.Platforms.AddRange(
                    new Platform() { Name = "DotNet", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Java", Publisher = "Qwerty", Cost = "Free"},
                    new Platform() { Name = "Docker", Publisher = "Cloud", Cost = "Free" }
                    );
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine(" --> We already have data");
            }
        }
    }
}
