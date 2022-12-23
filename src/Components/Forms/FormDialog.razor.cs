using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using RemindMeal.Services;

namespace RemindMeal.Components.Forms
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

        private void Cancel() => MudDialog.Cancel();

        protected EditContext editContext;

        protected TModel model;

        protected abstract Task OnValidSubmit();
    }
}