using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace MudBlazorTest.Components
{
    public sealed class AddDialog<TModel> : FormDialog<TModel>
    {
        protected override void InitializedEditContext()
        {
            editContext = new EditContext(Model);
        }

        protected override async Task OnValidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("console.log", Model.ToString());
            var model = await Repository.InsertAsync(Model);
            MudDialog.Close(DialogResult.Ok(model));
        }

    }
}