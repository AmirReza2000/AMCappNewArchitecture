using Domain.Features.Identity;
using Framework;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Messages;
using Services.Features.Common;
using Services.Features.Identity;
using System.Net;
using System.Text.RegularExpressions;
using ViewModels.Pages.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Controllers.Account;

[Route("api/account")]
[ApiController]
public class AccountController :
    BaseControllerModelWithDatabaseContext
{

    #region Constructor

    public AccountController(DatabaseContext DatabaseContext,
        UserNotificationService userNotificationService,
        HttpContextService httpContextService,
        UserManagerService userManagerService) :
        base(databaseContext: DatabaseContext)
    {
        UserNotificationService = userNotificationService;
        HttpContextService = httpContextService;
        _userManagerService = userManagerService;
    }

    #endregion /Constructor

    #region Properties

    private UserNotificationService UserNotificationService { get; init; }
    private HttpContextService HttpContextService { get; init; }
    private UserManagerService _userManagerService { get; init; }

    #endregion /Properties

    #region Register

    [HttpPost]
    [Route(template: "Register")]
    public async Task<ActionResult> RegisterAsync(
        RegisterViewModel ViewModel)
    {
        string errorMessage;


        // **************************************************
        var RemoteIP =
            HttpContextService.GetRemoteIpAddress();

        if (string.IsNullOrWhiteSpace(value: RemoteIP))
        {
            return BadRequest();

        }
        // **************************************************

        // **************************************************
        var cellPhoneNumber =
            ViewModel.CellPhoneNumber.Fix()!;

        var isCellPhoneNumberFound =
            await
            DatabaseContext.Users
            .Where(current => current.CellPhoneNumber != null
                && current.CellPhoneNumber == cellPhoneNumber)
            .AnyAsync()
            ;

        if (isCellPhoneNumberFound)
        {
            errorMessage = Resources.Messages.Errors.AlreadyExists;
            return BadRequest(new
            {
                errors = new
                {
                    cellPhoneNumber = new string[]
                    {
                        errorMessage
                    }
                }
            });
        }

        // **************************************************

        // **************************************************

        var password =
            ViewModel.Password.Fix()!;

        var user =
            new User
            (
            cellPhoneNumber: cellPhoneNumber,
            registerIP: RemoteIP);

        

        user.ResetSecurityKey();
        user.SetPassword(password);
        user.ResetCellPhoneNumberVerificationKey();

        var entityEntry =
            await
            DatabaseContext.AddAsync(entity: user);

        var affectedRows =
            await
            DatabaseContext.SaveChangesAsync();

        // **************************************************

        // **************************************************

        try
        {
            UserNotificationService
               .SendAccountVerifyLink(
                phoneNumber:
                user.CellPhoneNumber,
                key: user.SecurityKey.ToString());
        }
        catch
        {
        }

        // **************************************************

        var key = user.SecurityKey.ToString();

        return Created
            (uri:
            Constants.CommonRouting.VerifyPhoneNumber,
           new
           {
               result = new { 
                   key = key
               }
           });
    }

    #endregion /Register

    #region AccountVerify

    [HttpGet]
    [Route(template: "AccountVerify")]
    public async Task<ActionResult> AccountVerify(string key, string cellPhoneNumber)
    {
        // **************************************************
        key = key.Fix()!;
        cellPhoneNumber = cellPhoneNumber.Fix()!;
        if (key is null || cellPhoneNumber is null)
        {
            return NotFound();
        }

        key = key.Replace
            (oldValue: " ", newValue: string.Empty);
        cellPhoneNumber = cellPhoneNumber.Replace
            (oldValue: " ", newValue: string.Empty);
        // **************************************************

        // **************************************************
        System.Guid keyGuid;

        try
        {
            keyGuid =
                new System.Guid(g: key);
        }
        catch
        {
            return NotFound();
        }
        if (!Regex.IsMatch(cellPhoneNumber, Constants.RegularExpression.CellPhoneNumber))
        {
            return NotFound();
        }
        // **************************************************


        // **************************************************
        var foundedUser =
            await
            DatabaseContext.Users
            .Where(current => current.SecurityKey == keyGuid 
            && current.CellPhoneNumber == cellPhoneNumber)
            .FirstOrDefaultAsync();


        if (foundedUser == null)
        {
            return NotFound();
        }

        if (foundedUser.IsCellPhoneNumberVerified)
        {
            foundedUser.ResetSecurityKey();
            await DatabaseContext.SaveChangesAsync();
            return Conflict();
        }

        if (Framework.DateTime.TokenExpired(foundedUser.UpdateDateTime.UtcDateTime))
        {
            foundedUser.ResetSecurityKey();
            await DatabaseContext.SaveChangesAsync();
            return BadRequest();
        }

        // **************************************************

        // **************************************************
        foundedUser.IsCellPhoneNumberVerified = true;
        foundedUser.IsActive = true;
        foundedUser.ResetSecurityKey();

        await DatabaseContext.SaveChangesAsync();
        // **************************************************

        return Ok(
            new
            {
                title="1234"
            }) ;
    }
    #endregion /AccountVerfiy

    #region Login
    [HttpPost]
    [Route(template:"Login")]
    public async Task<ActionResult> Login(LoginViewModel ViewModel)
    {

        // **************************************************
        var PhoneNumber =
            ViewModel.CellPhoneNumber.Fix()!.ToLower();

        Domain.Features.Identity.User? foundedUser = null;

        foundedUser =
            await
            DatabaseContext.Users
            .Include(current => current.Role)
            .Where(current =>
            current.CellPhoneNumber.ToLower()
                == PhoneNumber)
            .FirstOrDefaultAsync();

        if (foundedUser is null)
        {
            return BadRequest();
        }
        // **************************************************

        // **************************************************
        var password =
            ViewModel.Password.Fix()!;

        var passwordHash =
            Framework.Security.Hashing.GetSha256(value: password);
        // **************************************************

        // **************************************************
        if (string.Compare(strA: foundedUser.Password,
            strB: passwordHash, ignoreCase: false) != 0)
        {
            return BadRequest() ;
        }
        // **************************************************

        // **************************************************
        if (foundedUser.IsCellPhoneNumberVerified == false)
        {
            var errorMessage =
                string.Format(format:
                Resources.Messages.Errors.PhoneNumberIsNotVerified);
            return StatusCode(((int)HttpStatusCode.UpgradeRequired));

        }

        if (foundedUser.IsActive == false)
        {
            return StatusCode(((int)HttpStatusCode.Forbidden));
        }
        
        if (foundedUser.Role!.IsActive == false)
        {
            return StatusCode(((int)HttpStatusCode.Forbidden));

        }
        // **************************************************

        var result =            
            _userManagerService.LoginAsync
            (user: foundedUser, rememberMe: ViewModel.RememberMe, log: true,
            loginType: Domain.Features.Identity.Enums.AuthenticationTypeEnum.Internal);

        if (result == null)
        {
            return StatusCode(((int)HttpStatusCode.InternalServerError));
        }
        else
        {
            return Ok(new
            {
                key = result
            }); 
        }
    }
    #endregion /Login

    #region Methods

    #endregion /Methods
}
