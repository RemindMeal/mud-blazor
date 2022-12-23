using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemindMeal.Data;
using RemindMeal.Model;

namespace RemindMealTest;

public class Tests
{
    private RemindMealDbContext? context;

    [SetUp]
    public async Task SetupAsync()
    {
        // Build our services container
        var services = new ServiceCollection();

        // Define the DbSet and Server Type for the DbContext Factory
        services.AddDbContext<RemindMealDbContext>(options
            => options.UseSqlite($"Data Source=RemindMealDatabase-{Guid.NewGuid().ToString()}"));

        var rootProvider = services.BuildServiceProvider();

        //define a scoped container
        var providerScope = rootProvider.CreateScope();
        var provider = providerScope.ServiceProvider;
        context = provider.GetRequiredService<RemindMealDbContext>();
        await context.Database.EnsureCreatedAsync();
        Assert.NotNull(context);
    }

    [Test]
    public async Task TestDbContext()
    {
        await context.Categories.AddAsync(new Category { Name = "Entrées" });

        await context.SaveChangesAsync();

        var categories = await context.Categories.ToListAsync();

        Assert.That(categories.Count, Is.EqualTo(1));
        Assert.That(categories[0].Name, Is.EqualTo("Entrées"));
    }
}