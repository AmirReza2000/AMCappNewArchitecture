﻿namespace ViewModels.Pages.Account;

public sealed class RegisterViewModel : object
{
    #region Constructor
    public RegisterViewModel() : base()
    {
    }
    #endregion /Constructor

    #region Properties

    #region public string? Password { get; set; }
    /// <summary>
    /// گذرواژه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (Name = nameof(Resources.DataDictionary.Password),
        ResourceType = typeof(Resources.DataDictionary))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.Password,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

    [System.ComponentModel.DataAnnotations.RegularExpression
        (pattern: Constants.RegularExpression.Password,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]

    [System.ComponentModel.DataAnnotations.DataType
        (dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
    public string? Password { get; set; }
    #endregion /public string? Password { get; set; }

    #region public string? ConfirmPassword { get; set; }
    /// <summary>
    /// تکرار گذرواژه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (Name = nameof(Resources.DataDictionary.ConfirmPassword),
        ResourceType = typeof(Resources.DataDictionary))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    //[System.ComponentModel.DataAnnotations.Compare
    //	(otherProperty: "Password",
    //	ErrorMessageResourceType = typeof(Resources.Messages.Validations),
    //	ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

    [System.ComponentModel.DataAnnotations.Compare
        (otherProperty: nameof(Password),
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Compare))]

    [System.ComponentModel.DataAnnotations.DataType
        (dataType: System.ComponentModel.DataAnnotations.DataType.Password)]
    public string? ConfirmPassword { get; set; }
    #endregion /public string? ConfirmPassword { get; set; }

    #region public string? CellPhoneNumber { get; set; }
    /// <summary>
    /// شماره تلفن همراه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumber))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

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

    #endregion /Properties
}
