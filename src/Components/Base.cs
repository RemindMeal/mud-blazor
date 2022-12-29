using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace RemindMeal.Components;

public abstract class RemindMealComponent : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }
}