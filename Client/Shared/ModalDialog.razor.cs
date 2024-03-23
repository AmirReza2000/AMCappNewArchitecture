using Microsoft.AspNetCore.Components;

namespace Client.Shared
{
    public partial class ModalDialog : ComponentBase
    {
        public ModalDialog()
        {
            //Title = "Defult";
        }

        //[Parameter]
        //public string Title { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private string modalDisplay = "none;";
        private string modalClass = string.Empty;
        private bool showBackdrop = false;

        public void Open()
        {
            modalDisplay = "block";
            modalClass = "show";
            showBackdrop = true;
        }

        public void Close()
        {
            modalDisplay = "none";
            modalClass = string.Empty;
            showBackdrop = false;
        }
    }
}
