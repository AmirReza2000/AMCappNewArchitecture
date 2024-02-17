using Framework;

namespace Domain.Features.Identity;

public class User :
    Seedwork.Entity,
    Domain.Seedwork.Abstractions.IEntityHasIsActive,
    Domain.Seedwork.Abstractions.IEntityHasOrdering,
    Domain.Seedwork.Abstractions.IEntityHasIsUndeletable,
    Domain.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
    #region Constructor
    public User(string username, string cellPhoneNumber,
        string registerIP) : base()
    {
        Ordering = 10_000;
        Username = username;
        CellPhoneNumber = cellPhoneNumber;
        RegisterIP = registerIP;
        UpdateDateTime = InsertDateTime;
        RoleId = Constants.BaseTableItem.Role.SimpleUser;
        // **************************************************
        LoginLogs =
                new System.Collections.Generic.List<LoginLog>();

    }
    #endregion /Constructor

    #region Properties

    #region public System.Guid? RoleId { get; set; }
    /// <summary>
    /// نقش کاربر
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Role))]
    public System.Guid? RoleId { get; set; }
    #endregion /public System.Guid? RoleId { get; set; }

    #region public virtual Common.BaseTableItem? Role { get; private set; }
    /// <summary>
    /// نقش کاربر
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Role))]
    public virtual Common.BaseTableItem? Role { get; private set; }
    #endregion /public virtual Common.BaseTableItem? Role { get; private set; }

    #region public bool IsActive { get; set; }
    /// <summary>
    /// وضعیت
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsActive))]
    public bool IsActive { get; set; }
    #endregion /public bool IsActive { get; set; }

    #region public bool IsUndeletable { get; set; }
    /// <summary>
    /// غیر قابل حذف
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsUndeletable))]
    public bool IsUndeletable { get; set; }
    #endregion /public bool IsUndeletable { get; set; }

    #region public bool IsCellPhoneNumberVerified { get; set; }
    /// <summary>
    /// شماره تلفن همراه تایید شده است
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.IsCellPhoneNumberVerified))]
    public bool IsCellPhoneNumberVerified { get; set; }
    #endregion /public bool IsCellPhoneNumberVerified { get; set; }


    #region public int Ordering { get; set; }
    /// <summary>
    /// چیدمان
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Ordering))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    [System.ComponentModel.DataAnnotations.Range
        (minimum: 1, maximum: 100_000,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
    public int Ordering { get; set; }
    #endregion /public int Ordering { get; set; }


    #region public string Username { get; set; }
    /// <summary>
    /// شناسه کاربری
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Username))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.Username,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.Username,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]
	public string Username { get; set; }
    #endregion /public string Username { get; set; }

    #region public string RegisterIP { get; set; }
    /// <summary>
    /// آی‌پی کاربر در زمان ثبت‌نام
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.RegisterIP))]

    [System.ComponentModel.DataAnnotations.Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.IP,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string RegisterIP { get; set; }
    #endregion /public string RegisterIP { get; set; }

    #region public string? NationalCode { get; set; }
    /// <summary>
    /// کد ملی
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.NationalCode))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.FixedLength.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

    [System.ComponentModel.DataAnnotations.RegularExpression
        (pattern: Constants.RegularExpression.NationalCode,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.NationalCode))]
    public string? NationalCode { get; set; }
    #endregion /public string? NationalCode { get; set; }

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
    public string CellPhoneNumber { get; set; }
    #endregion /public string? CellPhoneNumber { get; set; }

    #region public string? Password { get; private set; }
    /// <summary>
    /// گذرواژه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Password))]

    [System.ComponentModel.DataAnnotations.MinLength
        (length: Constants.FixedLength.DatabasePassword,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.FixedLength.DatabasePassword,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string? Password { get; private set; }
    #endregion /public string? Password { get; private set; }

    #region public string? CellPhoneNumberVerificationKey { get; private set; }
    /// <summary>
    /// کد تایید شماره تلفن همراه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.CellPhoneNumberVerificationKey))]

    [System.ComponentModel.DataAnnotations.MinLength
        (length: Constants.MinLength.CellPhoneNumberVerificationKey,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MinLength))]

    [System.ComponentModel.DataAnnotations.MaxLength
        (length: Constants.MaxLength.CellPhoneNumberVerificationKey,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    public string? CellPhoneNumberVerificationKey { get; private set; }
    #endregion /public string? CellPhoneNumberVerificationKey { get; private set; }

    #region public System.Guid SecurityKey { get; private set; }
    /// <summary>
    /// کلید امنیتی
    /// </summary>
    public System.Guid SecurityKey { get; private set; }
    #endregion /public System.Guid SecurityKey { get; private set; }

    #region public System.DateTimeOffset? LastLoginDateTime { get; set; }
    /// <summary>
    /// آخرین زمان ورود به سامانه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.LastLoginDateTime))]

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
        (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public System.DateTimeOffset? LastLoginDateTime { get; set; }
    #endregion /public System.DateTimeOffset? LastLoginDateTime { get; set; }

    #region public System.DateTimeOffset UpdateDateTime { get; private set; }
    /// <summary>
    /// زمان ویرایش
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.UpdateDateTime))]

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
        (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public System.DateTimeOffset UpdateDateTime { get; private set; }
    #endregion /public System.DateTimeOffset UpdateDateTime { get; private set; }

    #region public System.DateTimeOffset? LastChangePasswordDateTime { get; private set; }
    /// <summary>
    /// آخرین زمان تغییر گذرواژه
    /// </summary>
    [System.ComponentModel.DataAnnotations.Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.LastChangePasswordDateTime))]

    [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
        (System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public System.DateTimeOffset? LastChangePasswordDateTime { get; private set; }
    #endregion /public System.DateTimeOffset? LastChangePasswordDateTime { get; private set; }

    #endregion /Properties

    #region Methods

    #region SetPassword()
    public void SetPassword(string password)
    {
        var passwordHash = Framework.Security
            .Hashing.GetSha256(value: password);

        LastChangePasswordDateTime = Framework.DateTime.Now;

        if (passwordHash == null)
            throw new ArgumentNullException(message: "passwordHash is null",
                new Exception("An error occurred while setting the password in the user class"));
        Password = passwordHash;

    }
    #endregion /SetPassword()

    #region ResetSecurityKey()
    public void ResetSecurityKey()
    {
        SecurityKey = System.Guid.NewGuid();
    }
    #endregion /ResetSecurityKey()

    #region ResetCellPhoneNumberVerificationKey()

    public void ResetCellPhoneNumberVerificationKey()
    {
        CellPhoneNumberVerificationKey=Framework.
            RandomValues.
                    GenerateRandomNumber(MinLength:Constants.MinLength.CellPhoneNumberVerificationKey,
                    MaxLength:Constants.MaxLength.CellPhoneNumberVerificationKey).ToString();
    }

    #endregion /ResetCellPhoneNumberVerificationKey()

    #region SetUpdateDateTime()
    public void SetUpdateDateTime()
    {
        UpdateDateTime = Framework.DateTime.Now;
    }
    #endregion /SetUpdateDateTime()

    #endregion /Methods

    #region Collections

    public virtual System.Collections.Generic.IList<LoginLog> LoginLogs { get; private set; }

    #endregion /Collections
}
