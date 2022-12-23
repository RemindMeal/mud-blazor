using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using RemindMeal.Services;

namespace RemindMeal.Components.Forms
{
    public abstract partial class FormDialog<TModel> : RemindMealComponent
    {
        [Inject]
        public IAsyncRepository<TModel> Repository { get; set; }

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