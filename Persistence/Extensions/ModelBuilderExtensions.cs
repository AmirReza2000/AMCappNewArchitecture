namespace Persistence.Extensions;

internal static class ModelBuilderExtensions : object
{
	#region Methods
	#region Seed()
	public static void Seed
		(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// تابع ذیل باید اول نوشته شود
		SeedBaseTables(modelBuilder: modelBuilder);

		SeedUsers(modelBuilder: modelBuilder);
	}
	#endregion /Seed()
	#region SeedBaseTables()
	private static void SeedBaseTables
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// **************************************************
		SeedBaseTables_Roles
			(modelBuilder: modelBuilder);
		// **************************************************

		
	}
	#endregion /SeedBaseTables()

	#region SeedBaseTables_Roles()
	private static void SeedBaseTables_Roles
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{

		Domain.Features.Common.BaseTable baseTable;
		// **************************************************
		// **************************************************
		// **************************************************
		baseTable =
			new(code: Domain.Features.Common.Enums.BaseTableEnum.Role,
			type: Domain.Features.Common.Enums.BaseTableTypeEnum.Enum)
			{
				IsActive = true,
			};

		modelBuilder.Entity<Domain.Features.Common.BaseTable>().HasData(data: baseTable);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		Domain.Features.Common.BaseTableItem baseTableItem;
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser))
			{
				IsActive = true,
				Ordering = 1000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.SimpleUser);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator))
			{
				IsActive = true,
				Ordering = 4000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.Administrator,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.Administrator);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))
			{
				IsActive = true,
				Ordering = 5000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.ApplicationOwner);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		baseTableItem =
			new(baseTableId: baseTable.Id, keyName:
			nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer))
			{
				IsActive = true,
				Ordering = 6000,
				Code = (int)Domain.Features.Identity.Enums.RoleEnum.Programmer,
			};

		baseTableItem.SetId(id:
			Constants.BaseTableItem.Role.Programmer);

		modelBuilder.Entity<Domain.Features.Common
			.BaseTableItem>().HasData(data: baseTableItem);		
		// **************************************************
		// **************************************************
		// **************************************************
	}
	#endregion /SeedBaseTables_Roles()

	#region SeedUsers()
	private static void SeedUsers
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{

		// **************************************************
		// **************************************************
		var userProgrammer =
			new Domain.Features.Identity.User
			(username: "Amirreza", cellPhoneNumber: "09903333615"
			, registerIP: "::1")
			{
				Ordering = 1000,
				IsActive = true,
				IsUndeletable = true,
				IsCellPhoneNumberVerified = true,
				RoleId =
					Constants.BaseTableItem.Role.Programmer,
			};
		userProgrammer.SetPassword("J6mgod5!8&85311#50@PCD15y3");
		userProgrammer.ResetSecurityKey();
		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: userProgrammer);
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		var user1 =
			new Domain.Features.Identity.User
			(username: "User1111", cellPhoneNumber: "09905956472"
			, registerIP: "::1")
			{
				Ordering = 1000,
				IsActive = true,
				IsUndeletable = true,
				IsCellPhoneNumberVerified = true,
				RoleId =
					Constants.BaseTableItem.Role.Programmer,
			};
		user1.SetPassword("god4335247");
		user1.ResetSecurityKey();
		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user1);
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		var user2 =
			new Domain.Features.Identity.User
			(username: "User2222", cellPhoneNumber: "09905881865"
			, registerIP: "::1")
			{
				Ordering = 1000,
				IsActive = true,
				IsUndeletable = true,
				IsCellPhoneNumberVerified = true,
				RoleId =
					Constants.BaseTableItem.Role.Programmer,
			};
		user2.SetPassword("god4335247");
		user2.ResetSecurityKey();
		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user2);
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		var user3 =
			new Domain.Features.Identity.User
			(username: "User3333", cellPhoneNumber: "09205956472"
			, registerIP: "::1")
			{
				Ordering = 1000,
				IsActive = true,
				IsUndeletable = true,
				IsCellPhoneNumberVerified = false,
				RoleId =
					Constants.BaseTableItem.Role.Programmer,
			};
		user3.SetPassword("god4335247");
		user3.ResetSecurityKey();
		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user3);
		// **************************************************
		// **************************************************
	}
	#endregion /SeedUsers()
	#endregion /Methods
}
