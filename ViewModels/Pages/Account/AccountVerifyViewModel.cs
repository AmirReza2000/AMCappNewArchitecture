using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account
{
	public class AccountVerifyViewModel
	{
		#region Constructor
		public AccountVerifyViewModel()
			: base()
		{
		}
        #endregion /Constructor

        #region Properties

        #region public string CellPhoneNumber { get; set; }
        /// <summary>
        /// شماره تلفن همراه
        /// </summary>
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.CellPhoneNumber))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constants.MaxLength.CellPhoneNumber,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

        [System.ComponentModel.DataAnnotations.RegularExpression
            (pattern: Constants.RegularExpression.CellPhoneNumber,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.CellPhoneNumber))]
        public string? CellPhoneNumber { get; set; }
        #endregion /public string? CellPhoneNumber { get; set; }

        #region public string? SecurityKey{ get; set; }

        [Required(AllowEmptyStrings = false)]
		public string? SecurityKey { get; set; }

		#endregion/#region public string? SecurityKey{ get; set; }

		#endregion /Properties
	}
}
