using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using RemindMeal.Model;
using RemindMeal.Services;

namespace RemindMeal.Components.Forms;

public sealed class EditFormDialog<TModel> : FormDialog<TModel> where TModel : IModel
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IAsyncRepository<TModel> Repository { get; set; }

    protected override async Task OnInitializedAsync()
    {
        model = await Repository.GetByIdAsync(Id);
        editContext = new EditContext(model);
        base.OnInitialized();
    }

    protected override async Task OnValidSubmit()
    {
        await JSRuntime.InvokeVoidAsync("console.log", $"Modifying {model}");
        var newModel = await Repository.UpdateAsync(Id, model);
        if (newModel != null)
        {
            await JSRuntime.InvokeVoidAsync("console.log", $"{newModel} has been successfully modified. Closing Dialog from EditFormDialog");
            MudDialog.Close(DialogResult.Ok(newModel));
            await JSRuntime.InvokeVoidAsync("console.log", "Dialog closed");
        }
    }
}