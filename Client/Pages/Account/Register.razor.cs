using Blazored.Toast.Services;
using Client.Infrastructure;
using Client.Layout;
using Client.Services.Contracts;
using Client.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Security.Principal;
using ViewModels.Pages.Account;

namespace Client.Pages.Account;

public partial class Register : ComponentBase
{
    #region Constructor
    public Register()
    {
        ModalDialog = new();
        displayPageMessages = new();
        //MessageErrors = new List<string>();
    }
    #endregion /Constructor
    #region Properties
    private RegisterViewModel? RegisterModel { get; set; }
    //private ModalDialog ModalDialog { get; set; }
    private ModalDialog ModalDialog { get; set; }
    private DisplayPageMessages displayPageMessages { get; set; }
    private bool Showalert { get; set; }
    //private IList<string>? MessageErrors { get; set; }
    [Inject]
    protected IAccountRepository? Account { get; set; }
    [Inject]
    protected IToastService? ToastService { get; set; }
    [Inject]
    private NavigationManager? _navigation { get; set; }
    #endregion Properties

    protected override void OnInitialized()
    {
        // var toastParameters = new ToastParameters().
        // Add(nameof(CustomToast.ToastLevel), ToastLevel.Success);
        // ToastService.ShowSuccess("tasti");
        if (RegisterModel is null)
        {
            RegisterModel = new();
        }
        Showalert = false;


    }
    private async Task HandleRegistration()
    {
        var result = await Account!.RegisterAsync(RegisterModel!);
        var statusCode = result!.status;
        displayPageMessages.ClearMessages();
        //var statusCode = 201;
        //Console.WriteLine(nameof(statusCode) + "::"  +statusCode.ToString());
        switch (statusCode)
        {
            case ((int)HttpStatusCode.Created):
                //ToastService!.ShowSuccess
                //(Resources.Messages.Successes.RegistrationDone);               
                ModalDialog.Open();
                ShouldRender();
                break;
            case int n when (n <= 499 && n >= 400):
                Console.WriteLine("in 400");
                if (result.errors == null)
                {
                    result.errors = new();
                    result.errors!.cellPhoneNumber!.Add
                        (Resources.Messages.Errors.BadRequest);
                    ShouldRender();
                    break;
                }
                displayPageMessages.AddMessageErrors(result.errors.cellPhoneNumber);
                displayPageMessages.AddMessageErrors(result.errors.password);
                displayPageMessages.AddMessageErrors(result.errors.confirmPassword);
                displayPageMessages.Refresh();
                break;
            case int n when (n <= 599 && n >= 501):
                if (result.errors == null)
                {
                    result.errors = new();
                }
                result.errors.
                    cellPhoneNumber.Add(Resources.Messages.Errors.BadGatewayError);
                displayPageMessages.AddMessageErrors(result.errors.cellPhoneNumber);
                displayPageMessages.Refresh();
                break;
            default:
                if (result.errors == null)
                {
                    result.errors = new();
                }
                result.errors.
                    cellPhoneNumber.Add(Resources.Messages.Errors.BadGatewayError);
                //_displayPageMessages.AddMessages(result.errors.username, MessageLevel.Error);
                displayPageMessages.Refresh();
                break;
        }
    }
    //public void AddMessageErrors(List<string> messages)
    //{
    //    foreach (var message in messages)
    //    {
    //        Console.WriteLine(message);
    //        Console.WriteLine($"MessageErrors.Count.ToString() :{MessageErrors.Count.ToString()}");

    //        // StateHasChanged();
    //    }
    //}
}
