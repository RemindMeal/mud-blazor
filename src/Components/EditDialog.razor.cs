using Microsoft.AspNetCore.Components;
using RemindMealData.Model;

namespace RemindMeal.Components;

public abstract class EditDialog<TModel> : ComponentBase where TModel : IModel
{
    [Parameter]
    public Guid Id { get; set; }
}