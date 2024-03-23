using Client.Services.Contracts;
using Constants;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using ViewModels.Pages.Account;
using ViewModels.Pages.Account.ModelState;

namespace Client.Services;

public class AccountRepository : ServiceBase, IAccountRepository
{
    #region Constructor
    public AccountRepository(HttpClient http,
        LogsService logsService) :
        base(httpClient: http, logsService: logsService)
    {
    }
	#endregion /Constructor

	#region Properties 

	#endregion /Properties

	#region Register
	public async Task<ModelState<AccountErrors,
        SuccessRegisterViewModel>?> RegisterAsync(RegisterViewModel registerViewModel)
    {
        var response =await PostAsync<RegisterViewModel,
            ModelState<AccountErrors, SuccessRegisterViewModel>>(url:
            CommonRouting.RegisterApi, viewModel: registerViewModel);
        if (response == null)
        {
            response = new ModelState
                <AccountErrors, SuccessRegisterViewModel>();
            response.status = ((int)HttpStatusCode.BadGateway);
            response.title=Resources.Messages.Errors.BadGatewayError;
        }
        return response;
    }
    #endregion /Register

	#region AccountVerify
	public async Task<ModelState> AccountVerifyAsync(string key, string cellPhoneNumber)
	{
       var response= await GetAsync<ModelState>
            (CommonRouting.AccountVerifyApi,$"key={key}&cellPhoneNumber={cellPhoneNumber}");
		if (response == null)
		{
			response = new ModelState();
			response.status = ((int)HttpStatusCode.BadGateway);
			response.title = Resources.Messages.Errors.BadGatewayError;
		}
		return response;
	}
    #endregion /AccountVerify


    #region LoginAsync
    public async Task<ModelState<AccountErrors, SuccessRegisterViewModel>> 
        LoginAsync(LoginViewModel loginViewModel)
    {
        Console.WriteLine("in login");
        
        Console.WriteLine($"null return");

        return null ;
    }
    #endregion /LoginAsync
}
