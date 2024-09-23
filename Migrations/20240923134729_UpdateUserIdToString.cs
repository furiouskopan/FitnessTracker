using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseLogs_Users_UserId",
                table: "ExerciseLogs");

            // Alter the Id column in Users (removing identity)
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Alter UserId column in ExerciseLogs
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ExerciseLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Add foreign key back after altering the columns
            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseLogs_Users_UserId",
                table: "ExerciseLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseLogs_Users_UserId",
                table: "ExerciseLogs");

            // Revert the Id column in Users
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            // Revert the UserId column in ExerciseLogs
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ExerciseLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Add foreign key back after reverting the columns
            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseLogs_Users_UserId",
                table: "ExerciseLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
