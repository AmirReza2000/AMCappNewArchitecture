﻿using Framework.SMS;

namespace Infrastructure.Settings;

public class SMSetting : ISMSetting
{
    public SMSetting()
    {
        ApiKey = string.Empty;
        OtpPattern = string.Empty;
    }
    public string ApiKey {  get; set; }
    public string OtpPattern { get; set; }
    public string ResetPasswordPattern { get; set; }
}
