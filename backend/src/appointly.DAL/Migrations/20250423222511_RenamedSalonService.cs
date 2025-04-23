using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedSalonService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SalonServices_SalonServiceId",
                table: "Appointments"
            );

            migrationBuilder.DropTable(name: "EmployeeSalonServices");

            migrationBuilder.DropTable(name: "SalonServices");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SalonServiceId",
                table: "Appointments"
            );

            migrationBuilder.AddColumn<int>(
                name: "TreatmentId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EmployeeTreatments",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    SalonServiceId = table.Column<int>(type: "integer", nullable: false),
                    TreatmentId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTreatments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_EmployeeTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTreatments_EmployeeId",
                table: "EmployeeTreatments",
                column: "EmployeeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTreatments_TreatmentId",
                table: "EmployeeTreatments",
                column: "TreatmentId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Treatments_TreatmentId",
                table: "Appointments"
            );

            migrationBuilder.DropTable(name: "EmployeeTreatments");

            migrationBuilder.DropTable(name: "Treatments");

            migrationBuilder.DropIndex(name: "IX_Appointments_TreatmentId", table: "Appointments");

            migrationBuilder.DropColumn(name: "TreatmentId", table: "Appointments");

            migrationBuilder.CreateTable(
                name: "SalonServices",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonServices", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "EmployeeSalonServices",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    SalonServiceId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalonServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalonServices_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_EmployeeSalonServices_SalonServices_SalonServiceId",
                        column: x => x.SalonServiceId,
                        principalTable: "SalonServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SalonServiceId",
                table: "Appointments",
                column: "SalonServiceId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalonServices_EmployeeId",
                table: "EmployeeSalonServices",
                column: "EmployeeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalonServices_SalonServiceId",
                table: "EmployeeSalonServices",
                column: "SalonServiceId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SalonServices_SalonServiceId",
                table: "Appointments",
                column: "SalonServiceId",
                principalTable: "SalonServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
