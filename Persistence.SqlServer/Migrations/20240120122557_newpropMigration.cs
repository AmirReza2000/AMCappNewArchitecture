using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class newpropMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    IsTestData = table.Column<bool>(type: "bit", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseTableItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseTableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<int>(type: "int", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsSynced = table.Column<bool>(type: "bit", nullable: false),
                    IsTestData = table.Column<bool>(type: "bit", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTableItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseTableItems_BaseTables_BaseTableId",
                        column: x => x.BaseTableId,
                        principalTable: "BaseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsUndeletable = table.Column<bool>(type: "bit", nullable: false),
                    IsCellPhoneNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RegisterIP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NationalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Password = table.Column<string>(type: "varchar(44)", unicode: false, maxLength: 44, nullable: false),
                    CellPhoneNumberVerificationKey = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    SecurityKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastLoginDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastChangePasswordDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BaseTableItems_RoleId",
                        column: x => x.RoleId,
                        principalTable: "BaseTableItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    LoginType = table.Column<int>(type: "int", nullable: false),
                    LogoutDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BaseTables",
                columns: new[] { "Id", "Code", "Color", "CoverImageUrl", "Icon", "ImageUrl", "InsertDateTime", "IsActive", "IsSynced", "IsTestData", "Ordering", "Type", "UpdateDateTime" },
                values: new object[] { new Guid("c558b71f-dca1-477b-9728-58f3e5081fc1"), 1100, null, null, null, null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(1896), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, 10000, 0, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(1896), new TimeSpan(0, 3, 30, 0, 0)) });

            migrationBuilder.InsertData(
                table: "BaseTableItems",
                columns: new[] { "Id", "BaseTableId", "Code", "Color", "CoverImageUrl", "Icon", "ImageUrl", "InsertDateTime", "IsActive", "IsSynced", "IsTestData", "KeyName", "Ordering", "UpdateDateTime" },
                values: new object[,]
                {
                    { new Guid("210262a3-b878-4fea-ae21-1cd3be57d355"), new Guid("c558b71f-dca1-477b-9728-58f3e5081fc1"), 400, null, null, null, null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2259), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "ApplicationOwner", 5000, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2259), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("29d863fe-8504-4a9e-8cd2-8efc2bfaaee2"), new Guid("c558b71f-dca1-477b-9728-58f3e5081fc1"), 0, null, null, null, null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2146), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "SimpleUser", 1000, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2146), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("c558b71f-dca1-477b-9728-58f3e5081fc1"), 900, null, null, null, null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2364), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "Programmer", 6000, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2364), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("c2539a70-ad1c-4c89-8bea-eaeb81615ff9"), new Guid("c558b71f-dca1-477b-9728-58f3e5081fc1"), 300, null, null, null, null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "Administrator", 4000, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 3, 30, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CellPhoneNumber", "CellPhoneNumberVerificationKey", "InsertDateTime", "IsActive", "IsCellPhoneNumberVerified", "IsUndeletable", "LastChangePasswordDateTime", "LastLoginDateTime", "NationalCode", "Ordering", "Password", "RegisterIP", "RoleId", "SecurityKey", "UpdateDateTime", "Username" },
                values: new object[] { new Guid("d2e07f37-f8c7-4328-87ab-85ee4bf61dde"), "+989903333615", null, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2410), new TimeSpan(0, 3, 30, 0, 0)), true, true, true, new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(3111), new TimeSpan(0, 3, 30, 0, 0)), null, null, 1000, "yiWZY9ZeObt6nMoNLcILU33J0TGH5kO613g+cwKtqFU=", "::1", new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2024, 1, 20, 15, 55, 51, 246, DateTimeKind.Unspecified).AddTicks(2410), new TimeSpan(0, 3, 30, 0, 0)), "Amirreza" });

            migrationBuilder.CreateIndex(
                name: "IX_BaseTableItems_BaseTableId",
                table: "BaseTableItems",
                column: "BaseTableId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_UserId",
                table: "LoginLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogs_UserIP",
                table: "LoginLogs",
                column: "UserIP");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CellPhoneNumber",
                table: "Users",
                column: "CellPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalCode",
                table: "Users",
                column: "NationalCode",
                unique: true,
                filter: "[NationalCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BaseTableItems");

            migrationBuilder.DropTable(
                name: "BaseTables");
        }
    }
}
