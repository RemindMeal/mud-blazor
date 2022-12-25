using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RemindMeal.Data;
using RemindMeal.Model;

namespace RemindMealTest;

public sealed class DataTests
{
    private RemindMealDbContext context;

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
    public async Task TestCategory()
    {
        await context.Categories.AddAsync(new Category { Name = "Entrées" });

        await context.SaveChangesAsync();

        var categories = await context.Categories.ToListAsync();

        Assert.That(categories.Count, Is.EqualTo(1));
        Assert.That(categories[0].Name, Is.EqualTo("Entrées"));
    }

    [Test]
    public async Task TestFriends()
    {
        await context.Friends.AddAsync(new Friend { Name = "Francoise", Surname = "Lafond" });
        await context.Friends.AddAsync(new Friend { Name = "Nicolas", Surname = "Meresse" });

        await context.SaveChangesAsync();

        var friends = await context.Friends.ToListAsync();

        Assert.That(friends.Count, Is.EqualTo(2));

        var francoise = await context.Friends.SingleAsync(f => f.Surname == "Lafond");

        Assert.That(francoise.Name, Is.EqualTo("Francoise"));
    }

    [Test]
    public async Task TestRecipe()
    {
        await context.Recipes.AddAsync(new Recipe { Name = "Canard laqué", Category = new Category { Name = "Plat" } });
        await context.SaveChangesAsync();

        var recipes = await context.Recipes.ToListAsync();

        Assert.That(recipes.Count, Is.EqualTo(1));
        Assert.That(recipes[0].Name, Is.EqualTo("Canard laqué"));
        Assert.That(recipes[0].Category.Name, Is.EqualTo("Plat"));
    }
    
    [Test]
    public async Task TestMeal()
    {
        var starters = new Category { Name = "Entrées" };
        var mains = new Category { Name = "Plat" };
        var desserts = new Category { Name = "Desserts" };

        var corn = new Recipe
        {
            Name = "Maïs doux",
            Description = "Classique",
            Category = starters
        };
        var chicken = new Recipe
        {
            Name = "Poulet riz",
            Description = "La base",
            Category = mains
        };
        var tiramisu = new Recipe
        {
            Name = "Tiramisu",
            Description = "Très bon dessert italien",
            Category = desserts
        };

        var francoise = new Friend
        {
            Name = "Françoise",
            Surname = "Lafond"
        };
        var bapt = new Friend
        {
            Name = "Baptiste",
            Surname = "Berthe"
        };

        var meal = new Meal
        {
            Date = DateTime.Today
        };
        meal.Dishes.Add(new Dish
        {
            Recipe = corn,
            Meal = meal
        });
        meal.Dishes.Add(new Dish
        {
            Recipe = chicken,
            Meal = meal
        });
        meal.Dishes.Add(new Dish
        {
            Recipe = tiramisu,
            Meal = meal
        });

        meal.Presences.Add(new Presence
        {
            Friend = francoise,
            Meal = meal
        });
        meal.Presences.Add(new Presence
        {
            Friend = bapt,
            Meal = meal
        });

        await context.Meals.AddAsync(meal);
        await context.SaveChangesAsync();

        meal = await context.Meals.SingleAsync();

        Assert.That(meal.Date, Is.EqualTo(DateTime.Today));
        Assert.That(meal.Dishes.Count, Is.EqualTo(3));
        Assert.That(meal.Presences.Count, Is.EqualTo(2));
    }
}