using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
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
                    RegisterIP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NationalCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Password = table.Column<string>(type: "varchar(44)", unicode: false, maxLength: 44, nullable: true),
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
                values: new object[] { new Guid("120c6ba8-50c4-4c19-ae4b-705d770323c5"), 1100, null, null, null, null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(161), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, 10000, 0, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(161), new TimeSpan(0, 3, 30, 0, 0)) });

            migrationBuilder.InsertData(
                table: "BaseTableItems",
                columns: new[] { "Id", "BaseTableId", "Code", "Color", "CoverImageUrl", "Icon", "ImageUrl", "InsertDateTime", "IsActive", "IsSynced", "IsTestData", "KeyName", "Ordering", "UpdateDateTime" },
                values: new object[,]
                {
                    { new Guid("210262a3-b878-4fea-ae21-1cd3be57d355"), new Guid("120c6ba8-50c4-4c19-ae4b-705d770323c5"), 400, null, null, null, null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(379), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "ApplicationOwner", 5000, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(379), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("29d863fe-8504-4a9e-8cd2-8efc2bfaaee2"), new Guid("120c6ba8-50c4-4c19-ae4b-705d770323c5"), 0, null, null, null, null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(321), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "SimpleUser", 1000, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(321), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("120c6ba8-50c4-4c19-ae4b-705d770323c5"), 900, null, null, null, null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(397), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "Programmer", 6000, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(397), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("c2539a70-ad1c-4c89-8bea-eaeb81615ff9"), new Guid("120c6ba8-50c4-4c19-ae4b-705d770323c5"), 300, null, null, null, null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(361), new TimeSpan(0, 3, 30, 0, 0)), true, false, false, "Administrator", 4000, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(361), new TimeSpan(0, 3, 30, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CellPhoneNumber", "CellPhoneNumberVerificationKey", "InsertDateTime", "IsActive", "IsCellPhoneNumberVerified", "IsUndeletable", "LastChangePasswordDateTime", "LastLoginDateTime", "NationalCode", "Ordering", "Password", "RegisterIP", "RoleId", "SecurityKey", "UpdateDateTime" },
                values: new object[,]
                {
                    { new Guid("335d69bd-71af-48be-a998-ea6b781a6401"), "09903333615", null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(418), new TimeSpan(0, 3, 30, 0, 0)), true, true, true, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(556), new TimeSpan(0, 3, 30, 0, 0)), null, null, 1000, "aeLSS3ibnGMK3yHluBGVdWDZWKt/AT0qtfbblLEDVkY=", "::1", new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("326f8ede-eb0d-4e2e-ba3b-3e08a204a194"), new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(418), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("542be3a5-fece-4e3c-98e7-b6e930603367"), "09905956472", null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(594), new TimeSpan(0, 3, 30, 0, 0)), true, true, true, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(609), new TimeSpan(0, 3, 30, 0, 0)), null, null, 1000, "0cMQPZLYG+DUWiIBTlivf/Jp5g+Y7BRnW/uTnTdjSWE=", "::1", new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("b371ae0c-8d6f-4da6-9e94-21a07ed76734"), new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(594), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("9802378a-eba8-44f8-9b15-c2f0e305d67a"), "09205956472", null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 3, 30, 0, 0)), true, false, true, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(668), new TimeSpan(0, 3, 30, 0, 0)), null, null, 1000, "0cMQPZLYG+DUWiIBTlivf/Jp5g+Y7BRnW/uTnTdjSWE=", "::1", new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("f67db87d-0942-44e0-a7a8-623fa5bcef12"), new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(659), new TimeSpan(0, 3, 30, 0, 0)) },
                    { new Guid("cec20a1a-f4d3-4381-abcb-10e24435bfe3"), "09905881865", null, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(631), new TimeSpan(0, 3, 30, 0, 0)), true, true, true, new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(640), new TimeSpan(0, 3, 30, 0, 0)), null, null, 1000, "0cMQPZLYG+DUWiIBTlivf/Jp5g+Y7BRnW/uTnTdjSWE=", "::1", new Guid("b7bb7615-1d74-452c-a3a0-e85076b04c20"), new Guid("c12b4a5a-3557-4552-8143-e9d832d57f06"), new DateTimeOffset(new DateTime(2024, 3, 14, 15, 22, 28, 789, DateTimeKind.Unspecified).AddTicks(631), new TimeSpan(0, 3, 30, 0, 0)) }
                });

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
