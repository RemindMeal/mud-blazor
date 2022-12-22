using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazorTest.Services;

namespace MudBlazorTest.Components.Forms
{
    public abstract partial class FormDialog<TModel> : ComponentBase
    {
        [Inject]
        public IAsyncRepository<TModel> Repository { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public RenderFragment<TModel> Fields { get; set; }

        [Parameter]
        public TModel Model { get; set; }

        private void Cancel() => MudDialog.Cancel();

        protected EditContext editContext;

        protected override void OnInitialized()
        {
            editContext = new EditContext(Model);
        }

        protected abstract Task OnValidSubmit();
    }
}