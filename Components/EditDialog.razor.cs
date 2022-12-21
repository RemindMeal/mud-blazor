using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazorTest.Model;

namespace MudBlazorTest.Components
{
    public sealed class EditDialog<TModel> : FormDialog<TModel> where TModel : IModel
    {
        protected override void InitializedEditContext()
        {
            editContext = new EditContext(Model);
        }

        protected override async Task OnValidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("console.log", $"Modifying {Model}");
            var model = await Repository.UpdateAsync(Model.Id, Model);
            MudDialog.Close(DialogResult.Ok(model));
        }
    }
}