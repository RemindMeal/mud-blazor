using RemindMeal.Data;
using RemindMeal.Model;

namespace RemindMeal.Services;

public sealed class IngredientRepository : AsyncRepository<Ingredient, string>
{
    public IngredientRepository(RemindMealDbContext context) : base(context, context.Ingredients)
    { }

    public override async Task<Ingredient> UpdateAsync(int id, Ingredient newIngredient)
    {
        var ingredient = await _dbSet.FindAsync(id);
        if (ingredient == null)
            return null;

        ingredient.Name = newIngredient.Name;

        _dbSet.Update(ingredient);
        await _context.SaveChangesAsync();

        return ingredient;
    }

    public override async Task<Ingredient> DeleteAsync(Ingredient model)
    {
        return await DeleteAsync(model.Id);
    }

    protected override string OrderKeySelector(Ingredient t) => t.Name;
}