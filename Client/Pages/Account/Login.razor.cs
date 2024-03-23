using Client.Services.Contracts;
using Client.Shared;
using Constants;
using Microsoft.AspNetCore.Components;
using System.Net;
using ViewModels.Pages.Account;
using ViewModels.Pages.Account.ModelState;
using System.Net.Http.Json;
using Blazored.Toast.Services;
using Client.Infrastructure.Providers;

namespace Client.Pages.Account
{
    public partial class Login
    {
        #region Constructor
        public Login()
        {
        }
        #endregion /Constructor

        #region Properties
        [Parameter]
        public string? ReturnUrl { get; set; }

        private LoginViewModel _viewModel { get; set; }
        private DisplayPageMessages displayPageMessages { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        private IAccountRepository _accountRepository { get; set; }
        [Inject]
        private HttpClient _httpClient { get; set; }
        [Inject]
        private CustomAuthenticationStateProvider _customAuthenticationStateProvider { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        #endregion /Properties

        protected override void OnInitialized()
        {
            _viewModel = new();
        }
        public async Task LoginAsync()
        {
            // HTTP Client -> Backend (Username, Password) -> Request JWT Token
            Console.WriteLine("in login2");
            var http = await _httpClient.PostAsJsonAsync(CommonRouting.LoginApi, _viewModel);
            switch (((int)http.StatusCode))
            {
                case (((int)HttpStatusCode.OK)):
                    ModelState<AccountErrors,
                                   SuccessRegisterViewModel> modelState = new();
                    modelState.result = new();
                    modelState.result.key = await http.Content.ReadAsStringAsync();
                    Console.WriteLine($"key:{modelState.result.key}");
                    var result = await
                    _customAuthenticationStateProvider
                        .LoginAsync(jwtToken: modelState.result.key);
                    if (result)
                    {
                        var url = string.Empty;

                        if (string.IsNullOrWhiteSpace(value: ReturnUrl) == false)
                        {
                            url = ReturnUrl;
                        }
                        _toastService.ShowSuccess(
                            Resources.Messages.Successes.Login);
                        _navigationManager.NavigateTo
                            (uri: url, forceLoad: false);
                    }
                    break;
                case (((int)HttpStatusCode.BadRequest)):
                    displayPageMessages.ClearMessages();
                    displayPageMessages.AddMessageError(
                       Resources.Messages.Errors.InvalidUsernameOrPassword
                        );
                    displayPageMessages.Refresh();

                    break;
                case (((int)HttpStatusCode.UpgradeRequired)):
                    displayPageMessages.ClearMessages();
                    displayPageMessages.AddMessageError(
                       Resources.Messages.Errors.PhoneNumberIsNotVerified
                        );
                    displayPageMessages.Refresh();

                    break;
                case (((int)HttpStatusCode.Forbidden)):
                    displayPageMessages.ClearMessages();
                    displayPageMessages.AddMessageError(
                       Resources.Messages.Errors.UserIsNotActive
                        );
                    displayPageMessages.Refresh();

                    break;
                case (((int)HttpStatusCode.UnprocessableEntity)):
                    var jsonSerializerOptions =
                new System.Text.Json.JsonSerializerOptions
                {
                    MaxDepth = 10,
                    PropertyNameCaseInsensitive = true,

                };
                    var modelState1 = await http.Content.
                        ReadFromJsonAsync<ModelState<AccountErrors, SuccessRegisterViewModel>>();
                    displayPageMessages.ClearMessages();
                    displayPageMessages.AddMessageErrors
                        (modelState1!.errors!.cellPhoneNumber);
                    displayPageMessages.AddMessageErrors
                        (modelState1!.errors!.password);
                    displayPageMessages.Refresh();
                    break;
                case int n when (n <= 599 && n >= 501):
                    displayPageMessages.ClearMessages();
                    displayPageMessages.
                        AddMessageError(
                        Resources.Messages.Errors.BadGatewayError);
                    displayPageMessages.Refresh();
                    break;
                default:
                    displayPageMessages.
                     AddMessageError(
                     Resources.Messages.Errors.UnexpectedError);
                    displayPageMessages.Refresh();
                    break;

            }

        }
    }
}
