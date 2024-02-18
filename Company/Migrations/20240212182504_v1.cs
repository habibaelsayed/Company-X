using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Dnum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MGRSSN = table.Column<int>(type: "int", nullable: true),
                    MGRStartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Dnum);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    SSN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "money", nullable: false),
                    SuperSSN = table.Column<int>(type: "int", nullable: true),
                    Dno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_Dno",
                        column: x => x.Dno,
                        principalTable: "Departments",
                        principalColumn: "Dnum");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SuperSSN",
                        column: x => x.SuperSSN,
                        principalTable: "Employees",
                        principalColumn: "SSN");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Pnum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PLoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Pnum);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_DeptNum",
                        column: x => x.DeptNum,
                        principalTable: "Departments",
                        principalColumn: "Dnum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependants",
                columns: table => new
                {
                    EmpSSN = table.Column<int>(type: "int", nullable: false),
                    DependantName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependants", x => new { x.EmpSSN, x.DependantName });
                    table.ForeignKey(
                        name: "FK_Dependants_Employees_EmpSSN",
                        column: x => x.EmpSSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "works_For",
                columns: table => new
                {
                    EmpSSN = table.Column<int>(type: "int", nullable: false),
                    Pnum = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works_For", x => new { x.EmpSSN, x.Pnum });
                    table.ForeignKey(
                        name: "FK_works_For_Employees_EmpSSN",
                        column: x => x.EmpSSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_works_For_Projects_Pnum",
                        column: x => x.Pnum,
                        principalTable: "Projects",
                        principalColumn: "Pnum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MGRSSN",
                table: "Departments",
                column: "MGRSSN",
                unique: true,
                filter: "[MGRSSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Dno",
                table: "Employees",
                column: "Dno");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SuperSSN",
                table: "Employees",
                column: "SuperSSN");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeptNum",
                table: "Projects",
                column: "DeptNum");

            migrationBuilder.CreateIndex(
                name: "IX_works_For_Pnum",
                table: "works_For",
                column: "Pnum");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_MGRSSN",
                table: "Departments",
                column: "MGRSSN",
                principalTable: "Employees",
                principalColumn: "SSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_MGRSSN",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Dependants");

            migrationBuilder.DropTable(
                name: "works_For");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
