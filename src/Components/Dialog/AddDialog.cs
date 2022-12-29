using Microsoft.AspNetCore.Components;
using RemindMeal.Model;
using RemindMeal.Services;

namespace RemindMeal.Components.Dialog;

public abstract class AddDialog<TModel> : ComponentBase where TModel : IModel
{
    [Inject]
    protected IAsyncRepository<TModel> Repository { get; set; }

    protected async Task OnAdd(TModel model)
    {
        await Repository.InsertAsync(model);
    }
}