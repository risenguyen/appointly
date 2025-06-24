using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTreatmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTreatments");

            migrationBuilder.AddColumn<int>(
                name: "TreatmentType",
                table: "Treatments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatmentType",
                table: "Treatments");

            migrationBuilder.CreateTable(
                name: "EmployeeTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    TreatmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTreatments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTreatments_EmployeeId",
                table: "EmployeeTreatments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTreatments_TreatmentId",
                table: "EmployeeTreatments",
                column: "TreatmentId");
        }
    }
}
