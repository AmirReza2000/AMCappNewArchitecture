using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account.ModelState
{
    public class AccountErrors
    {
        public AccountErrors()
        {
            password = new();
            cellPhoneNumber = new();
            confirmPassword = new();
        }
        public List<string> cellPhoneNumber { get; set; }
        public List<string> password { get; set; }
        public List<string> confirmPassword { get; set; }
    }
}
