using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SalonServiceId", table: "EmployeeTreatments");

            migrationBuilder.DropColumn(name: "SalonServiceId", table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonServiceId",
                table: "EmployeeTreatments",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "SalonServiceId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}
