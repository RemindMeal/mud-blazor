using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazorTest.Model;
using MudBlazorTest.Services;

namespace MudBlazorTest.Components
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
            InitializedEditContext();
        }

        protected abstract void InitializedEditContext();
        protected abstract Task OnValidSubmit();
    }
}