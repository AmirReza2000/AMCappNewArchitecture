using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account
{
    public class VerifyPhoneNumberViewModel : object
    {
        #region Constructor
        public VerifyPhoneNumberViewModel() 
            : base()
        {
        }
        #endregion /Constructor

        #region Properties

        #region public string? CellPhoneNumber { get; set; }
        /// <summary>
        /// شماره تلفن همراه
        /// </summary>           
        public string? CellPhoneNumber { get; set; }
        #endregion /public string? CellPhoneNumber { get; set; }  

        #region public string? OTPCode{ get; set; }
        /// <summary>
        /// کد اعتبارسنجی
        /// </summary> 
        [System.ComponentModel.DataAnnotations.Display
         (Name = nameof(Resources.DataDictionary.CellPhoneNumberVerificationKey),
         ResourceType = typeof(Resources.DataDictionary))]

        [System.ComponentModel.DataAnnotations.Required
         (AllowEmptyStrings = false,
         ErrorMessageResourceType = typeof(Resources.Messages.Validations),
         ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
         (length: Constants.MaxLength.CellPhoneNumberVerificationKey,
         ErrorMessageResourceType = typeof(Resources.Messages.Validations),
         ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
        [System.ComponentModel.DataAnnotations.MinLength
         (length: Constants.MinLength.CellPhoneNumberVerificationKey,
         ErrorMessageResourceType = typeof(Resources.Messages.Validations),
         ErrorMessageResourceName = nameof(Resources.Messages.Validations.MinLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
         (pattern: Constants.RegularExpression.JustDigits,
         ErrorMessageResourceType = typeof(Resources.Messages.Validations),
         ErrorMessageResourceName = nameof(Resources.Messages.Validations.JustDigits))]
       
        public string? OTPCode { get; set; }
        #endregion /public string? OTPCode{ get; set; }

        #region public string? SecurityKey{ get; set; }

        [Required(AllowEmptyStrings =false)]  
        public string? SecurityKey{ get; set; }

        #endregion/#region public string? SecurityKey{ get; set; }

        #region public string? Username { get; set; }
        /// <summary>
        /// شناسه کاربری
        /// </summary>       

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false)]
        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Constants.RegularExpression.Username,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
        public string? Username { get; set; }
        #endregion /public string? Username { get; set; }

        #endregion /Properties
    }
}
