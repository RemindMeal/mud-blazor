using Microsoft.EntityFrameworkCore;
using RemindMeal.Data;

namespace RemindMeal.Services;

public abstract class AsyncRepository<TModel, TOrderKey> : IAsyncRepository<TModel> where TModel : class
{
    protected readonly RemindMealDbContext _context;
    protected readonly DbSet<TModel> _dbSet;

    protected AsyncRepository(RemindMealDbContext context, DbSet<TModel> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public virtual async Task<List<TModel>> GetListAsync()
    {
        return await Task.Run(() => _dbSet.OrderBy(OrderKeySelector).ToList());
    }

    public virtual IQueryable<TModel> GetQueryable()
    {
        return _dbSet.AsQueryable().OrderBy(OrderKeySelector).AsQueryable();
    }

    protected abstract TOrderKey OrderKeySelector(TModel t);

    public virtual async Task<TModel> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public virtual async Task<TModel> InsertAsync(TModel model)
    {
        _dbSet.Add(model);
        Console.WriteLine($"Inserting {model}");
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine($"A DB update exception has been raised: {e}");
        }

        return model;
    }

    public async Task<TModel> DeleteAsync(int id)
    {
        var model = await _dbSet.FindAsync(id);

        if (model == null)
            return null;

        _dbSet.Remove(model);
        await _context.SaveChangesAsync();

        return model;
    }

    public abstract Task<TModel> DeleteAsync(TModel model);

    public abstract Task<TModel> UpdateAsync(int id, TModel m);
}