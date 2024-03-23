using Blazored.Toast.Services;
using Client.Services.Contracts;
using Client.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.RegularExpressions;

namespace Client.Pages.Account
{
    public partial class AccountVerify
    {
        #region Constructor
        #endregion /Constructor
        #region Properties
        [Parameter]
        public string? key { get; set; }
        [Parameter]
        public string? cellPhoneNumber { get; set; }
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }
        [Inject]
        private IAccountRepository? _accountRepository { get; set; }
        [Inject]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected IToastService ToastService { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #endregion /Properties
        protected async override Task OnParametersSetAsync()
        {
            if (key == null | cellPhoneNumber == null)
            {
                return;
            }
            if (!Guid.TryParse(key, out var GuidKey) ||
                !Regex.IsMatch(cellPhoneNumber!, Constants.RegularExpression.CellPhoneNumber))
            {
                return;
            }
            var result = await _accountRepository!.AccountVerifyAsync(key: key, cellPhoneNumber: cellPhoneNumber!);
            var statusCode = result!.status;
            //var statusCode = 201;
            switch (statusCode)
            {
                case ((int)HttpStatusCode.OK):
                    Console.WriteLine("on 200");
                    ToastService.ShowSuccess(Resources.Messages.Successes.AccountVerified);
                    NavigationManager!.NavigateTo(Constants.CommonRouting.Login);
                    break;
                case int n when (n <= 499 && n >= 400):
                    if (n == 404)
                    {
                        Console.WriteLine("on 404");

                        return;
                    }
                    if (n == 409)
                    {
                        Console.WriteLine("on 409");

                        NavigationManager!.NavigateTo(Constants.CommonRouting.Login);
                        ToastService.ShowError(Resources.Messages.Successes.YourCellPhoneNumberHasBeenAlreadyVerified);
                    }
                    if (n == 400)
                    {
                        Console.WriteLine("on 400");

                        NavigationManager!.NavigateTo(Constants.CommonRouting.ResendVerifyPhoneNumberToken);
                        ToastService.ShowError(Resources.Messages.Errors.TokenExpired);
                    }
                    break;
                case int n when (n <= 599 && n >= 501):
                    Console.WriteLine("on 500");

                    ToastService.ShowError(Resources.Messages.Errors.BadGatewayError);
                    NavigationManager!.NavigateTo("/");
                    break;
                default:
                    Console.WriteLine("on defult");

                    ToastService.ShowError(Resources.Messages.Errors.UnexpectedError);
                    NavigationManager!.NavigateTo("/");
                    break;
            }
        }


    }
}
