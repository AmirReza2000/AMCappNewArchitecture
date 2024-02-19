using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants;

internal class ApiCommonRouting
{   /// <summary>
    /// Error 404
    /// </summary>
    public const string NotFound = "/Errors/Error404";

    /// <summary>
    /// Error 403
    /// </summary>
    public const string Forbidden = "/Errors/Error403";

    /// <summary>
    /// Error 400
    /// </summary>
    public const string BadRequest = "/Errors/Error400";

    /// <summary>
    /// Error 500
    /// </summary>
    public const string InternalServerError = "/Errors/Error500";



    /// <summary>
    /// Root Index
    /// </summary>
    public const string RootIndex = "/Index";

    #region Account
    /// <summary>
    /// Login
    /// </summary>
    public const string Controller = "/Api/Account";

    /// <summary>
    /// Login
    /// </summary>
    public const string Login = "/Login";

    /// <summary>
    /// Logout
    /// </summary>
    public const string Logout = "/Logout";

    /// <summary>
    /// Google Login
    /// </summary>
    public const string GoogleLogin = "/Login";

    /// <summary>
    /// Register
    /// </summary>
    public const string Register = "Register";
   // public const string Register = "Register";

    /// <summary>
    /// Verify PhoneNumber
    /// </summary>
    public const string VerifyPhoneNumber = $"{Controller}/VerifyPhoneNumber";

    /// <summary>
    /// Send Again Email Address Verification Key
    /// </summary>
    public const string
        SendAgainEmailAddressVerificationKey =
        $"{Controller}/SendAgainEmailAddressVerificationKey";
    /// <summary>
    /// Resend Verify PhoneNumber Token
    /// </summary>
    public const string
        ResendVerifyPhoneNumberToken =
        $"{Controller}/ResendVerifyPhoneNumberToken";
    #endregion /Account

    /// <summary>
    /// Current Index
    /// </summary>
    public const string CurrentIndex = "Index";

    /// <summary>
    /// Dashboard
    /// </summary>
    public const string Dashboard = "/Dashboard";

    
}
