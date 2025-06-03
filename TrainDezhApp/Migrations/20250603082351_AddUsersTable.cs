using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainDezhApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3892), new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3892), new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3892) });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "database_settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3922));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(4004));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(4007));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3826));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3835));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3836));

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(5012), "Администратор", true, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", 1, "root" });

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3867));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 8, 23, 50, 963, DateTimeKind.Utc).AddTicks(3868));

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6399), new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6398), new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6405), new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6405), new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6405) });

            migrationBuilder.UpdateData(
                table: "database_settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6428));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6330));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6339));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6368));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 35, 31, 552, DateTimeKind.Utc).AddTicks(6377));
        }
    }
}
