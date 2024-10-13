using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_driven_backend.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 23, 3, 4, 705, DateTimeKind.Utc).AddTicks(7490),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 16, 23, 1, 34, 427, DateTimeKind.Utc).AddTicks(1480));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 23, 1, 34, 427, DateTimeKind.Utc).AddTicks(1480),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 16, 23, 3, 4, 705, DateTimeKind.Utc).AddTicks(7490));
        }
    }
}
