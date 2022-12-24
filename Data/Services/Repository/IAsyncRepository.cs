namespace RemindMealData.Services;

public interface IAsyncRepository<TModel>
{
    Task<List<TModel>> GetListAsync();
    Task<TModel> GetFirstAsync();
    Task<TModel?> GetByIdAsync(Guid id);
    Task<TModel?> InsertAsync(TModel model);
    Task<TModel?> UpdateAsync(Guid id, TModel m);
    Task<TModel?> DeleteAsync(Guid id);
    Task<TModel?> DeleteAsync(TModel model);
}
