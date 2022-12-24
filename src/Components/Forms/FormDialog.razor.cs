using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RemindMeal.Components.Forms
{
    public abstract partial class FormDialog<TModel> : RemindMealComponent
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public RenderFragment<TModel> Fields { get; set; }

        private void Cancel() => MudDialog.Cancel();

        protected TModel model { get; set; }

        protected EditContext editContext;

        protected abstract Task OnValidSubmit();
    }
}