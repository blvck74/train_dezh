using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDezhApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(271), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "accidents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "OccurredAt", "ReportedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(279), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(278), new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(278) });

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
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(241));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "works",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 3, 7, 26, 11, 648, DateTimeKind.Utc).AddTicks(250));
        }
    }
}
