namespace Infrastructure.Settings;

public class ApplicationSettings : object
{
    #region Static Fields

    public static readonly string KeyName = nameof(ApplicationSettings);

    #endregion /Static Fields

    #region Constructor
    public ApplicationSettings() : base()
    {
        sMSetting = new SMSetting();
        tokenProfile = new TokenProfile();
    }
    #endregion /Constructor

    #region Properties

    public bool SiteHasSsl { get; set; }
    public string[]? ActivationKeys { get; set; }
    public string? CaptchaImageEncryptionKey { get; set; }
    public string? ConnectionString { get; set; }
    public SMSetting sMSetting { get; set; }
    public TokenProfile tokenProfile { get; set; }
    #endregion /Properties
}
