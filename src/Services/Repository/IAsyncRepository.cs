namespace RemindMeal.Services;

public interface IAsyncRepository<TModel>
{
    Task<List<TModel>> GetListAsync();
    IQueryable<TModel> GetQueryable();
    Task<TModel> GetByIdAsync(int id);
    Task<TModel> InsertAsync(TModel model);
    Task<TModel> UpdateAsync(int id, TModel m);
    Task<TModel> DeleteAsync(int id);
    Task<TModel> DeleteAsync(TModel model);
}
