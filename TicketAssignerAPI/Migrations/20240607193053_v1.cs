using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketAssignerAPI.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "App");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "App",
                newName: "Users");
        }
    }
}
