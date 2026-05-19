using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberSuffix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Members");
        }
    }
}
