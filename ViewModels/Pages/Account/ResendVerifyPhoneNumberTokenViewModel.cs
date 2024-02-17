using Constants;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account
{
	public class ResendVerifyPhoneNumberTokenViewModel
	{
		#region Constructor
		public ResendVerifyPhoneNumberTokenViewModel() : base()
		{
		}
		#endregion /Constructor

		#region Properties

		#region	public string UsernameOrPhoneNumber { get; set; }

		[Display(
			ResourceType =typeof(DataDictionary),
			Name =nameof(DataDictionary.UsernameOrPhoneNumber)
			)]
		[Required(
			AllowEmptyStrings =false,
			ErrorMessageResourceType =typeof(Validations),			
			ErrorMessageResourceName =nameof(Validations.RequiredGeneric))]
		[MaxLength
		(length: MaxLength.Username,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLengthGeneric))]

		[RegularExpression
		(pattern: RegularExpression.Username,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Name))]
		public string? UsernameOrPhoneNumber { get; set; }
		#endregion /public string UsernameOrPhoneNumber { get; set; }


		#endregion /Properties
	}
}
