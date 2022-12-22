using Microsoft.JSInterop;
using MudBlazor;
using MudBlazorTest.Model;

namespace MudBlazorTest.Components.Forms
{
    public sealed class EditFormDialog<TModel> : FormDialog<TModel> where TModel : IModel
    {
        protected override async Task OnValidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("console.log", $"Modifying {Model}");
            var model = await Repository.UpdateAsync(Model.Id, Model);
            if (model != null)
            {
                await JSRuntime.InvokeVoidAsync("console.log", $"{Model} has been successfully modified. Closing Dialog from EditFormDialog");
                MudDialog.Close(DialogResult.Ok(model));
                await JSRuntime.InvokeVoidAsync("console.log", "Dialog closed");
            }
        }
    }
}