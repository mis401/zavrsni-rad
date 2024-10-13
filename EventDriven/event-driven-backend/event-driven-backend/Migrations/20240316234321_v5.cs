using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_driven_backend.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 23, 43, 20, 314, DateTimeKind.Utc).AddTicks(2029),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 16, 23, 7, 55, 937, DateTimeKind.Utc).AddTicks(3837));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 16, 23, 7, 55, 937, DateTimeKind.Utc).AddTicks(3837),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 16, 23, 43, 20, 314, DateTimeKind.Utc).AddTicks(2029));
        }
    }
}
