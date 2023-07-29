using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class adminetc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Exercises");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
