using Domain.Features.Identity;
using Framework;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Features.Common;
using Services.Features.Identity;
using ViewModels.Pages.Account;

namespace Server.Controllers.Account;

[Route("api/account")]
[ApiController]
public class AccountController :
    BaseControllerModelWithDatabaseContext
{

    #region Constructor

    public AccountController(DatabaseContext DatabaseContext,
        UserNotificationService userNotificationService,
        HttpContextService httpContextService) :
        base(databaseContext: DatabaseContext)
    {
        UserNotificationService = userNotificationService;
        HttpContextService = httpContextService;
    }

    #endregion /Constructor

    #region Properties

    private UserNotificationService UserNotificationService { get; init; }
    private HttpContextService HttpContextService { get; init; }

    #endregion /Properties

    #region Register

    [HttpPost]
    [Route(template: "register")]
    public async Task<ActionResult> RegisterAsync(
        RegisterViewModel ViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // **************************************************
        var RemoteIP =
            HttpContextService.GetRemoteIpAddress();

        if (string.IsNullOrWhiteSpace(value: RemoteIP))
        {
            return BadRequest();

        }
        // **************************************************

        // **************************************************
        var username =
            ViewModel.Username.Fix()!.ToLower();

        var isUsernameFound =
            await
            DatabaseContext.Users
            .Where(current =>
                 current.Username.ToLower() == username
                 && current.IsCellPhoneNumberVerified == true)
            .AnyAsync()
            ;

        if (isUsernameFound)
        {
            var errorMessage = nameof(Resources.Messages.Errors.AlreadyExists);
            ModelState.AddModelError(key: "errors", errorMessage: errorMessage);
                return BadRequest(ModelState);
        }
        // **************************************************

        // **************************************************
        var cellPhoneNumber =
            ViewModel.CellPhoneNumber.Fix()!;

        var isCellPhoneNumber =
            await
            DatabaseContext.Users
            .Where(current => current.CellPhoneNumber != null
                && current.CellPhoneNumber == cellPhoneNumber)
            .AnyAsync()
            ;

        if (isCellPhoneNumber)
        {
            var errorMessage = nameof(Resources.Messages.Errors.AlreadyExists);
            ModelState.AddModelError(key: "errors", errorMessage: errorMessage);
            return BadRequest(ModelState);
        }

        // **************************************************

        // **************************************************

        var password =
            ViewModel.Password.Fix()!;

        var user =
            new User
            (username: username,
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
               .SendPhoneNumberVerifyCode(
                CellPhoneNumber:
                user.CellPhoneNumber,
                CellPhoneNumberVerificationKey:
                user.CellPhoneNumberVerificationKey!);
        }
        catch
        {
        }

        // **************************************************


        var key = user.SecurityKey.ToString();

        return Created
            (uri:
            Constants.CommonRouting.VerifyPhoneNumber,
           new { key = key, username = user.Username });
    }

    #endregion /Register

    #region Methods

    #endregion /Methods
}
