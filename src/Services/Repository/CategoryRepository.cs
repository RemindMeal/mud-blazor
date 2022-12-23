using MudBlazorTest.Data;
using MudBlazorTest.Model;

namespace MudBlazorTest.Services
{
    public sealed class CategoryRepository : AsyncRepository<Category, string>
    {
        public CategoryRepository(MudBlazorTestDbContext context) : base(context, context.Categories)
        {}

        public async override Task<Category> UpdateAsync(int id, Category newType)
        {
            var type = await _dbSet.FindAsync(id);
            if (type == null)
                return null;

            if (type.Name != newType.Name)
                type.Name = newType.Name;

            _dbSet.Update(type);
            await _context.SaveChangesAsync();
        
            return type;
        }

        public override async Task<Category> DeleteAsync(Category model)
        {
            return await DeleteAsync(model.Id);
        }

        protected override string OrderKeySelector(Category t) => t.Name;
    }
}