using Kavenegar;
using Kavenegar.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SMS;

public static class Utility : object
{
    private static KavenegarApi Provider { get; set; }
    static Utility()
    {
        Provider = new KavenegarApi("ApiKey");
    }
    public static int SendOTP(ISMSetting setting, string Receptor, string OTPcode)
    {
        Provider.ApiKey = setting.ApiKey;
        var result = Provider.VerifyLookup(Receptor, OTPcode, setting.OtpPattern);
        return result.Status;
    }
    public static int SendResetPasswordToken(ISMSetting setting,string Receptor,string username, string siteUrl, string key)
    {
        Provider.ApiKey = setting.ApiKey;
        var result = Provider.VerifyLookup(receptor:Receptor, 
            token: username, token2: siteUrl,token3: key,template: setting.ResetPasswordPattern);
        return result.Status;
    }
    //public Task SendSMS(ISMSetting setting,string Receptor, string Message)
    //{
    //    var date = DateTime.Now;
    //    Provider.Send("0990", Receptor, Message);
    //    return Task.CompletedTask;

    //}
}

