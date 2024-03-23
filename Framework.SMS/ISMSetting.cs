using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SMS
{
    public interface ISMSetting
    {
        public string ApiKey { get; set; }
        public string OtpPattern { get; set; }
        public string ResetPasswordPattern { get; set;}
        public string AccountVerifyLink { get; set; }

    }
}
