using Microsoft.EntityFrameworkCore;
using MudBlazorTest.Data;
using MudBlazorTest.Model;

namespace MudBlazorTest.Services
{
    public sealed class RecipeRepository : AsyncRepository<Recipe, string>
    {
        public RecipeRepository(MudBlazorTestDbContext context) : base(context, context.Recipes)
        {}

        public async override Task<List<Recipe>> GetListAsync()
        {
            return await _dbSet.Include(r => r.Category).OrderBy(r => r.Name).ToListAsync();
        }

        public override async Task<Recipe> GetByIdAsync(int id)
        {
            return await _dbSet.Include(r => r.Category).SingleAsync(r => r.Id == id);
        }

        public override async Task<Recipe> InsertAsync(Recipe recipe)
        {
            _context.Entry(recipe.Category).State = EntityState.Unchanged;
            _dbSet.Add(recipe);
            try {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine($"A DB update exception has been raised: {e}");
            }

            return recipe;
        }

        public override async Task<Recipe> UpdateAsync(int id, Recipe newRecipe)
        {
            var recipe = await _dbSet.FindAsync(id);
            if (recipe == null)
                return null;
 
            recipe.Name = newRecipe.Name;
            recipe.Description = newRecipe.Description;
            recipe.Category = newRecipe.Category;

            _dbSet.Update(recipe);
            try {
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Error raised during update : {e}");
            }
        
            return recipe;
        }

        protected override string OrderKeySelector(Recipe t) => t.Name;

        public override async Task<Recipe> DeleteAsync(Recipe model)
        {
            return await DeleteAsync(model.Id);
        }
    }
}