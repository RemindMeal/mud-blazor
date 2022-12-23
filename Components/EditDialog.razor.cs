using Microsoft.AspNetCore.Components;
using MudBlazorTest.Model;

namespace MudBlazorTest.Components;

public abstract class EditDialog<TModel> : ComponentBase where TModel : IModel
{
    [Parameter]
    public int Id { get; set; }
}