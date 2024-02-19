using Domain.Features.Identity;
using Infrastructure.Settings;

namespace Services.Features.Identity;

public class UserNotificationService
{
    #region Constructor
    public UserNotificationService(Infrastructure.Settings.ApplicationSettings applicationSettings, Common.HttpContextService httpContextService)
    {
        _applicationSettings = applicationSettings;
        HttpContextService = httpContextService;
    }
    #endregion /Constructor

    #region Properties
    private Common.HttpContextService HttpContextService { get; init; }
    private Infrastructure.Settings.ApplicationSettings _applicationSettings { get; }
    #endregion /Properties

    #region Methods

    #region SendPhoneNumberVerifyCodeAsync(string CellPhoneNumber, string CellPhoneNumberVerificationKey)
    public void SendPhoneNumberVerifyCode(string CellPhoneNumber, string CellPhoneNumberVerificationKey)
    {
        Framework.SMS.
            Utility.SendOTP(setting: _applicationSettings.sMSetting,
                 Receptor: CellPhoneNumber, OTPcode: CellPhoneNumberVerificationKey);
    }
    #endregion /SendCellPhoneNumberVerificationCodeAsync(string CellPhoneNumber, string CellPhoneNumberVerificationKey)

    #region SendResetPasswordTokenToPhoneNumber(string phoneNumber,string key)
    public void SendResetPasswordTokenToPhoneNumber(string phoneNumber,string username, string key)
    {
        var siteUrl =
            HttpContextService.GetCurrentHostUrl();
        Framework.SMS.
            Utility.SendResetPasswordToken(setting: _applicationSettings.sMSetting,
                Receptor: phoneNumber, username: username, siteUrl: siteUrl!, key: key);
    }
    #endregion /SendResetPasswordTokenToPhoneNumber()
    #endregion /Methods

}
