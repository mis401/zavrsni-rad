using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_driven_backend.Migrations
{
    /// <inheritdoc />
    public partial class a4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 17, 14, 50, 35, 230, DateTimeKind.Utc).AddTicks(4363),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 17, 14, 48, 24, 391, DateTimeKind.Utc).AddTicks(2880));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 17, 14, 48, 24, 391, DateTimeKind.Utc).AddTicks(2880),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 17, 14, 50, 35, 230, DateTimeKind.Utc).AddTicks(4363));
        }
    }
}
