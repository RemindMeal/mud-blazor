using Microsoft.AspNetCore.Components;
using RemindMeal.Model;

namespace RemindMeal.Components.Dialog;

public abstract class EditDialog<TModel> : ComponentBase where TModel : IModel
{
    [Parameter]
    public int Id { get; set; }
}