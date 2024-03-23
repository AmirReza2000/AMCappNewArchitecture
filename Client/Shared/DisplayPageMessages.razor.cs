using Microsoft.AspNetCore.Components;

namespace Client.Shared
{
    public partial class DisplayPageMessages : ComponentBase
    {
        public DisplayPageMessages()
        {
            MessageErrors = new List<string>();
        }
        protected override void OnInitialized()
        {
        }
        private IList<string>? MessageErrors { get; set; }

        public void AddMessageErrors(List<string> messages)
        {
            foreach (var message in messages)
            {
                MessageErrors!.Add(message);
            }
        }
        public void AddMessageError(string message)
        {
            MessageErrors!.Add(message);
        }
        public void ClearMessages()
        {
            if (MessageErrors is not null)
            {
                MessageErrors!.Clear();
            }
        }
        public void Refresh()
        {
            StateHasChanged();
        }
    }
}
