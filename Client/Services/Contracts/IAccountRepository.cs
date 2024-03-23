using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Account;
using ViewModels.Pages.Account.ModelState;

namespace Client.Services.Contracts
{
    public interface IAccountRepository
    {
		public Task<ModelState<AccountErrors,
            SuccessRegisterViewModel>?> RegisterAsync(RegisterViewModel registerViewModel);
		public Task<ModelState> AccountVerifyAsync(string key , string cellPhoneNumber);

        public Task<ModelState<AccountErrors,
            SuccessRegisterViewModel>> LoginAsync(LoginViewModel loginViewModel);

    }
}
