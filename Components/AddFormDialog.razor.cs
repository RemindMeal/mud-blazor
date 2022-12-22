using Microsoft.JSInterop;
using MudBlazor;

namespace MudBlazorTest.Components
{
    public sealed class AddFormDialog<TModel> : FormDialog<TModel>
    {
        protected override async Task OnValidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("console.log", Model.ToString());
            var model = await Repository.InsertAsync(Model);
            MudDialog.Close(DialogResult.Ok(model));
        }
    }
}