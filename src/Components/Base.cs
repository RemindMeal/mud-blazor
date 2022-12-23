using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RemindMeal.Components;

public abstract class RemindMealComponent : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }
}