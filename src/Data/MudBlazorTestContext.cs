using Microsoft.EntityFrameworkCore;
using RemindMeal.Model;

namespace RemindMeal.Data;

public class RemindMealDbContext : DbContext
{
    public RemindMealDbContext(DbContextOptions<RemindMealDbContext> options) : base(options)
    { }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Recipe
        modelBuilder.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
        modelBuilder.Entity<Recipe>().Property(r => r.Name).IsRequired();

        // Ingredient
        modelBuilder.Entity<Ingredient>().HasIndex(i => i.Name).IsUnique();

        // Category
        // modelBuilder.Entity<Category>().HasIndex(t => t.Name).IsUnique();
        modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired();

        // Meal <--> Recipe via Cooking
        modelBuilder.Entity<Dish>().HasKey(x => new { x.MealId, x.RecipeId });
        modelBuilder.Entity<Dish>()
            .HasOne(cooking => cooking.Meal)
            .WithMany(meal => meal.Dishes)
            .HasForeignKey(cooking => cooking.MealId);
        modelBuilder.Entity<Dish>()
            .HasOne(cooking => cooking.Recipe)
            .WithMany(recipe => recipe.Dishes)
            .HasForeignKey(cooking => cooking.RecipeId);

        // Meal <--> Friend via Presence
        modelBuilder.Entity<Presence>().HasKey(p => new { p.MealId, p.FriendId });
        modelBuilder.Entity<Presence>()
            .HasOne(p => p.Friend)
            .WithMany(f => f.Presences)
            .HasForeignKey(p => p.FriendId);
        modelBuilder.Entity<Presence>()
            .HasOne(p => p.Meal)
            .WithMany(m => m.Presences)
            .HasForeignKey(p => p.MealId);

        // Recipe <--> Category
        modelBuilder.Entity<Recipe>()
            .HasOne<Category>(r => r.Category)
            .WithMany(c => c.Recipes)
            .IsRequired();

        // Recipe <--> Ingredient
        modelBuilder.Entity<Recipe>()
            .HasMany<Ingredient>(r => r.Ingredients)
            .WithMany(i => i.Recipes);

    }
}