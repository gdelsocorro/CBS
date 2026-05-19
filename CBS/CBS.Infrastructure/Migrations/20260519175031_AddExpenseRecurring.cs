using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseRecurring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurrenceEndsOn",
                table: "Expenses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceSourceId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecurrenceType",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurrenceEndsOn",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurrenceSourceId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RecurrenceType",
                table: "Expenses");
        }
    }
}
