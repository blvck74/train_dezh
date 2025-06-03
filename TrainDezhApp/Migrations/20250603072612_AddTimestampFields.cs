using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDezhApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "works",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "accidents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "accidents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(271), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(271), null });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(279), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(278), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(278), null });

            migrationBuilder.UpdateData(
                table: "database_settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(299));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(316));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(207));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(217));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(241), null });

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(249), null });

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(250), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "works");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "accidents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "accidents");

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6134), new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6134) });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6142), new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6143) });

            migrationBuilder.UpdateData(
                table: "database_settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(5894));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6174));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6193));

            migrationBuilder.UpdateData(
                table: "materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 1,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6059));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6066));

            migrationBuilder.UpdateData(
                table: "staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "HiredAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6068));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6098));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6105));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6106));
        }
    }
}
