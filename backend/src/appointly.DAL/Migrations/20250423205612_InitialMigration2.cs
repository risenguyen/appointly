using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appointly.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalonService_Employee_EmployeeId",
                table: "EmployeeSalonService");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalonService_SalonService_SalonServiceId",
                table: "EmployeeSalonService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalonService",
                table: "SalonService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSalonService",
                table: "EmployeeSalonService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "SalonService",
                newName: "SalonServices");

            migrationBuilder.RenameTable(
                name: "EmployeeSalonService",
                newName: "EmployeeSalonServices");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalonService_SalonServiceId",
                table: "EmployeeSalonServices",
                newName: "IX_EmployeeSalonServices_SalonServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalonService_EmployeeId",
                table: "EmployeeSalonServices",
                newName: "IX_EmployeeSalonServices_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalonServices",
                table: "SalonServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSalonServices",
                table: "EmployeeSalonServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    SalonServiceId = table.Column<int>(type: "integer", nullable: false),
                    ClientName = table.Column<string>(type: "text", nullable: false),
                    ClientPhone = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_SalonServices_SalonServiceId",
                        column: x => x.SalonServiceId,
                        principalTable: "SalonServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SalonServiceId",
                table: "Appointments",
                column: "SalonServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalonServices_Employees_EmployeeId",
                table: "EmployeeSalonServices",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalonServices_SalonServices_SalonServiceId",
                table: "EmployeeSalonServices",
                column: "SalonServiceId",
                principalTable: "SalonServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalonServices_Employees_EmployeeId",
                table: "EmployeeSalonServices");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalonServices_SalonServices_SalonServiceId",
                table: "EmployeeSalonServices");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalonServices",
                table: "SalonServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSalonServices",
                table: "EmployeeSalonServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "SalonServices",
                newName: "SalonService");

            migrationBuilder.RenameTable(
                name: "EmployeeSalonServices",
                newName: "EmployeeSalonService");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalonServices_SalonServiceId",
                table: "EmployeeSalonService",
                newName: "IX_EmployeeSalonService_SalonServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalonServices_EmployeeId",
                table: "EmployeeSalonService",
                newName: "IX_EmployeeSalonService_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalonService",
                table: "SalonService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSalonService",
                table: "EmployeeSalonService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalonService_Employee_EmployeeId",
                table: "EmployeeSalonService",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalonService_SalonService_SalonServiceId",
                table: "EmployeeSalonService",
                column: "SalonServiceId",
                principalTable: "SalonService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
