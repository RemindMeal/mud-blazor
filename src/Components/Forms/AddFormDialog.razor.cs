using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace RemindMeal.Components.Forms
{
    public sealed class AddFormDialog<TModel> : FormDialog<TModel> where TModel : new()
    {
        protected override void OnInitialized()
        {
            model = new TModel();
            editContext = new EditContext(model);
            base.OnInitialized();
        }
        protected override async Task OnValidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("console.log", model.ToString());
            var newModel = await Repository.InsertAsync(model);
            MudDialog.Close(DialogResult.Ok(newModel));
        }
    }
}