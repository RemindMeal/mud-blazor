using Microsoft.EntityFrameworkCore;

namespace MudBlazorTest.Data
{
    public class MudBlazorTestDbContext : DbContext
    {
        public MudBlazorTestDbContext(DbContextOptions<MudBlazorTestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}