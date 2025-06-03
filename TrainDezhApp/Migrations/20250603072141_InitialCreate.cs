using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainDezhApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Details = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ReportedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Equipment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Investigation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accidents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "database_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Host = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Database = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UseSSL = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectionTimeout = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_database_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    MinQuantity = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Supplier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PartNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsImportant = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shift_handovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShiftType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OutgoingOfficer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IncomingOfficer = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CurrentTasks = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ImportantNotes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    EquipmentStatus = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    SafetyIssues = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HandoverTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Signature = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_handovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    HiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    EmployeeNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsOnShift = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AssignedTo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Equipment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "accidents",
                columns: new[] { "Id", "Description", "Details", "Equipment", "Investigation", "Location", "OccurredAt", "ReportedAt", "ReportedBy", "ResolvedAt", "Severity", "Status" },
                values: new object[,]
                {
                    { 1, "Сход вагона на км 15", null, null, null, "км 15", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6134), new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6134), null, null, "Критическая", "Расследуется" },
                    { 2, "Неисправность светофора", null, "Светофор №12", null, "км 25", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6142), new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6143), null, null, "Средняя", "Устранена" }
                });

            migrationBuilder.InsertData(
                table: "database_settings",
                columns: new[] { "Id", "ConnectionTimeout", "Database", "Host", "LastUpdated", "Password", "Port", "UseSSL", "Username" },
                values: new object[] { 1, 30, "train_dezh", "localhost", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(5894), "", 5432, false, "postgres" });

            migrationBuilder.InsertData(
                table: "materials",
                columns: new[] { "Id", "Category", "Description", "LastUpdated", "Location", "MinQuantity", "Name", "PartNumber", "Price", "Quantity", "Status", "Supplier", "Unit" },
                values: new object[,]
                {
                    { 1, "Запчасти", null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6174), null, 5m, "Тормозные колодки", null, 2500.00m, 15m, "В наличии", null, "шт" },
                    { 2, "ГСМ", null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6193), null, 10m, "Масло моторное", null, 450.00m, 3m, "Заканчивается", null, "л" },
                    { 3, "Крепеж", null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6195), null, 20m, "Болты М12", null, 15.00m, 100m, "В наличии", null, "шт" }
                });

            migrationBuilder.InsertData(
                table: "staff",
                columns: new[] { "Id", "Department", "Email", "EmployeeNumber", "FirstName", "HiredAt", "IsOnShift", "LastName", "MiddleName", "Phone", "Position", "Status" },
                values: new object[,]
                {
                    { 1, "Отдел Г", null, "001", "Иван", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6059), true, "Петров", null, null, "Дежурный по станции", "Активен" },
                    { 2, "Отдел Г", null, "002", "Мария", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6066), true, "Сидорова", null, null, "Машинист", "Активен" },
                    { 3, "Отдел Г", null, "003", "Алексей", new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6068), false, "Козлов", null, null, "Слесарь", "Активен" }
                });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "Id", "AssignedTo", "CompletedAt", "CreatedAt", "Description", "DueDate", "Equipment", "Location", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Козлов А.", null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6098), null, null, "Локомотив №1234", null, "Средний", "Выполняется", "Плановое ТО локомотива №1234" },
                    { 2, "Сидорова М.", null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6105), null, null, "Вагон №5678", null, "Низкий", "Завершено", "Замена тормозных колодок" },
                    { 3, null, null, new DateTime(2025, 6, 3, 7, 21, 40, 903, DateTimeKind.Utc).AddTicks(6106), null, null, null, "км 25", "Высокий", "Новая", "Ремонт светофора" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_accidents_Severity",
                table: "accidents",
                column: "Severity");

            migrationBuilder.CreateIndex(
                name: "IX_accidents_Status",
                table: "accidents",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_materials_Category",
                table: "materials",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_materials_Status",
                table: "materials",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_notes_Category",
                table: "notes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_notes_Priority",
                table: "notes",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_shift_handovers_ShiftDate",
                table: "shift_handovers",
                column: "ShiftDate");

            migrationBuilder.CreateIndex(
                name: "IX_shift_handovers_ShiftType",
                table: "shift_handovers",
                column: "ShiftType");

            migrationBuilder.CreateIndex(
                name: "IX_staff_EmployeeNumber",
                table: "staff",
                column: "EmployeeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_staff_Status",
                table: "staff",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_works_Priority",
                table: "works",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_works_Status",
                table: "works",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accidents");

            migrationBuilder.DropTable(
                name: "database_settings");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "shift_handovers");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "works");
        }
    }
}
