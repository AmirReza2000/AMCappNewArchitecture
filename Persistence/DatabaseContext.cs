using System.Linq;
using Persistence.Extensions;

namespace Persistence;

public class DatabaseContext :
	Microsoft.EntityFrameworkCore.DbContext
{
	#region Constructor
	public DatabaseContext(Microsoft.EntityFrameworkCore
		.DbContextOptions<DatabaseContext> options) : base(options: options)
	{
		// تا قبل از اولین نسخه اصلی
		Database.EnsureCreated();

		// نوشتن دستورات ذیل کامل غلط است
		// لااقل در اولین باری که بانک‌اطلاعاتی
		// می‌خواهد ایجاد شود، کار نمی‌کند
		//// using Microsoft.EntityFrameworkCore;
		//if (Database.GetAppliedMigrations().Any())
		//{
		//	// using Microsoft.EntityFrameworkCore;
		//	Database.Migrate();
		//}

		// using Microsoft.EntityFrameworkCore;
		//Database.Migrate();
	}
	#endregion /Constructor

	#region Properties

	#region SmartHome


	#endregion /SmartHome#region 

	#region Common Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.BaseTable> BaseTables { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.BaseTableItem> BaseTableItems { get; set; }

	#endregion /Common Feature

	#region Identity Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LoginLog> LoginLogs { get; set; }

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.User> Users { get; set; }

	#endregion /Identity Feature

	#endregion /Properties

	#region Methods

	#region OnModelCreating()
	protected override void OnModelCreating
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly
			(assembly: typeof(DatabaseContext).Assembly);
		modelBuilder.Seed();
	}
	#endregion /OnModelCreating()

	#endregion /Methods
}
