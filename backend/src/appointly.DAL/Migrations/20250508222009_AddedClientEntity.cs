using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ClientName", table: "Appointments");

            migrationBuilder.DropColumn(name: "ClientPhone", table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "LastName"
            );

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ClientId",
                table: "Appointments",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ClientId",
                table: "Appointments"
            );

            migrationBuilder.DropTable(name: "Clients");

            migrationBuilder.DropIndex(name: "IX_Appointments_ClientId", table: "Appointments");

            migrationBuilder.DropColumn(name: "FirstName", table: "Employees");

            migrationBuilder.DropColumn(name: "ClientId", table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "FullName"
            );

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "Appointments",
                type: "text",
                nullable: true
            );
        }
    }
}
