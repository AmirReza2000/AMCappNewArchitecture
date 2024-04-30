using Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using ViewModels.Client;

namespace Client.Pages
{
    public partial class Home
    {
        #region Constractor
 
        #endregion /Constractor

        #region Properties
        private static int CurrentSpace { get; set; }
        private static  Confing? _confing { get; set; }
        private  HttpClient? _httpClient { get; set; }
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        #endregion /Properties

        protected override async  Task OnParametersSetAsync()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new System.Uri
                           (uriString: NavigationManager.BaseUri);
            }

            var respons = await _httpClient.GetAsync("/Confing.json");
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
            };
            if(_confing == null)
            {

            _confing = await respons.Content.ReadFromJsonAsync<Confing>();
            }
        }
        //protected override async Task OnInitializedAsync()
        //{
        //}
        private async Task ChangeButtomStutus(string Id,int index)
        {
            _confing.Outputs[index].Sts = 
                _confing.Outputs[index].Sts == "OFF" ? "ON" : "OFF";
            await JSRuntime.InvokeVoidAsync("changeButtomStutus", Id);
        }
        //private async Task OpenFullscreen()
        //{

        //}
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        if(firstRender)
            {
                await JSRuntime.InvokeVoidAsync("registerEventListener");

            }
        }
        
        [JSInvokable("thistast")]
        public void thistast()
        {
            Console.WriteLine("rrrrrrrrrrrrrrrrr");
        }
        //protected override async  Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if(firstRender)
        //    {
        //        await JSRuntime.InvokeVoidAsync("myFunction");
        //    }
        //}
        private void ChangeSpace(int i)
        {
            CurrentSpace = i;
        }
    }


}

