using Microsoft.EntityFrameworkCore;
using RemindMeal.Data;
using RemindMeal.Model;

namespace RemindMeal.Services;

public sealed class MealRepository : AsyncRepository<Meal, DateTime>
{
    public MealRepository(RemindMealDbContext context) : base(context, context.Meals)
    { }

    public override async Task<List<Meal>> GetListAsync()
    {
        return await _dbSet
            .Include(m => m.Presences)
                .ThenInclude(p => p.Friend)
            .Include(m => m.Dishes)
                .ThenInclude(d => d.Recipe)
            .OrderBy(m => m.Date).ToListAsync();
    }

    public override async Task<Meal> UpdateAsync(int id, Meal newMeal)
    {
        var meal = await _dbSet
            .Include(meal => meal.Dishes)
                .ThenInclude(dish => dish.Recipe)
            .Include(meal => meal.Presences)
                .ThenInclude(presence => presence.Friend)
            .Where(meal => meal.Id == id)
            .SingleOrDefaultAsync();

        if (meal == null)
            return null;

        meal.Date = newMeal.Date;
        RemoveRecipes(meal, newMeal);
        AddNewRecipes(meal, newMeal);
        RemoveGuests(meal, newMeal);
        AddNewGuests(meal, newMeal);

        _dbSet.Update(meal);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Error raised during meal update : {e}");
        }

        return meal;
    }

    private void RemoveRecipes(Meal meal, Meal newMeal)
    {
        var newMealRecipeIds = newMeal.Dishes.Select(dish => dish.RecipeId).ToHashSet();
        foreach (var dish in meal.Dishes)
        {
            if (!newMealRecipeIds.Contains(dish.RecipeId))
            {
                meal.Dishes.Remove(dish);
            }
        }
    }

    private void AddNewRecipes(Meal meal, Meal newMeal)
    {
        var mealRecipeIds = meal.Dishes.Select(dish => dish.RecipeId).ToHashSet();
        foreach (var newDish in newMeal.Dishes)
        {
            if (!mealRecipeIds.Contains(newDish.RecipeId))
            {
                meal.Dishes.Add(newDish);
            }
        }
    }

    private void RemoveGuests(Meal meal, Meal newMeal)
    {
        var newMealFriendIds = newMeal.Presences.Select(presence => presence.FriendId).ToHashSet();
        foreach (var presence in meal.Presences)
        {
            if (!newMealFriendIds.Contains(presence.FriendId))
            {
                meal.Presences.Remove(presence);
            }
        }
    }

    private void AddNewGuests(Meal meal, Meal newMeal)
    {
        var mealFriendIds = meal.Presences.Select(presence => presence.FriendId).ToHashSet();
        foreach (var newPresence in newMeal.Presences)
        {
            if (!mealFriendIds.Contains(newPresence.FriendId))
            {
                meal.Presences.Add(newPresence);
            }
        }
    }

    protected override DateTime OrderKeySelector(Meal meal) => (DateTime)meal.Date;

    public override async Task<Meal> DeleteAsync(Meal meal)
    {
        return await DeleteAsync(meal.Id);
    }
}