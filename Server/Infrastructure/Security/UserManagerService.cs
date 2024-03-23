using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Services.Features.Identity;

namespace Infrastructure.Security;

public class UserManagerService : BaseServiceWithDatabaseContext
{
    #region Constructor
    public UserManagerService
        (Persistence.DatabaseContext databaseContext,
        Services.Features.Common.HttpContextService httpContextService,
        Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor,
        JwtTokenService jwtTokenService) : base(databaseContext: databaseContext)
    {
        HttpContextService = httpContextService;
        HttpContextAccessor = httpContextAccessor;
        _jwtTokenService = jwtTokenService;
    }
    #endregion /Constructor

    #region Properties
    private JwtTokenService _jwtTokenService { get; set; }
	private Services.Features.Common.HttpContextService HttpContextService { get; init; }
	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; init; }

	#endregion /Properties

	#region Login()
	public  string
		LoginAsync(Domain.Features.Identity.User user, bool rememberMe,
		bool log, Domain.Features.Identity.Enums.AuthenticationTypeEnum loginType)
	{
		if (user.Role is null)
		{
			Console.WriteLine("1111");
			return null;
		}

		if (HttpContextAccessor.HttpContext is null)
		{
            Console.WriteLine("2222");
            return null;
		}

		// **************************************************
		var userIP =
			HttpContextService.GetRemoteIpAddress();

		if (userIP is null)
		{
            Console.WriteLine("3333");
            return null;
		}
		// **************************************************

		// **************************************************

		// **************************************************

		// **************************************************
		var claims =
			new System.Collections.Generic
			.List<System.Security.Claims.Claim>();

		System.Security.Claims.Claim claim;
		// **************************************************

		// **************************************************
		claim = new System.Security.Claims.Claim(type:
			System.Security.Claims.ClaimTypes.Role, value: user.Role.KeyName);

		claims.Add(item: claim);

		if(user.Role.Code.HasValue)
		{
			claim = new System.Security.Claims.Claim(type: Constants
				.ClaimKeyName.RoleCode, value: user.Role.Code.Value.ToString());

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		// نباید از دستور ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.Name, value: foundedUser.Username);

		claim = new System.Security.Claims.Claim(type:
			System.Security.Claims.ClaimTypes.Name, value: user.CellPhoneNumber);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		// نیازی نیست که از دستور ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.Email, value: user.EmailAddress);

		// **************************************************

		// **************************************************
		// نیازی نیست که از دستورات ذیل استفاده نماییم
		//claim = new System.Security.Claims.Claim(type:
		//	System.Security.Claims.ClaimTypes.NameIdentifier, value: user.Id.ToString());

		claim = new System.Security.Claims.Claim(type:
			Constants.ClaimKeyName.UserId, value: user.Id.ToString());

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		claim = new System.Security.Claims.Claim(type:
			Constants.ClaimKeyName.UserIP, value: userIP);

		claims.Add(item: claim);
		// **************************************************

		//// **************************************************
		//if (loginLog is not null)
		//{
		//	claim = new System.Security.Claims.Claim(type:
		//		Constants.ClaimKeyName.SessionId, value: loginLog.Id.ToString());

		//	claims.Add(item: claim);
		//}
		//// **************************************************

		// **************************************************
		//var claimsIdentity =
		//	new System.Security.Claims.ClaimsIdentity
		//	(claims: claims, authenticationType: Constants.Scheme.Default);
		// **************************************************

		// **************************************************
		//var claimsPrincipal =
		//	new System.Security.Claims
		//	.ClaimsPrincipal(identity: claimsIdentity);
		// **************************************************

		// **************************************************
		//var authenticationProperties = new Microsoft
		//	.AspNetCore.Authentication.AuthenticationProperties
		//{
		//	IsPersistent = rememberMe,
		//};
		// **************************************************

		// **************************************************
		var token=_jwtTokenService.CreateToken(claims);
        // **************************************************
        Console.WriteLine("5555");
        return token!;
	}
	#endregion /Login()
}
