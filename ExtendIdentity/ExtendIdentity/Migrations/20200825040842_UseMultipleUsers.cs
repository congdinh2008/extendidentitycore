using Microsoft.EntityFrameworkCore.Migrations;

namespace ExtendIdentity.Migrations
{
    public partial class UseMultipleUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AuditTrail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassAdmins",
                columns: table => new
                {
                    ClassAdminId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAdmins", x => x.ClassAdminId);
                    table.ForeignKey(
                        name: "FK_ClassAdmins_Users_ClassAdminId",
                        column: x => x.ClassAdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AllocationStatus = table.Column<int>(type: "int", nullable: false),
                    ForeignLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_Trainees_Users_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassAdmins");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropColumn(
                name: "AuditTrail",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
