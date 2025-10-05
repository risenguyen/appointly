using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Clients",
                type: "text",
                nullable: true);
        }
    }
}
