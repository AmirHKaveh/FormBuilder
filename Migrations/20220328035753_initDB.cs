using Microsoft.EntityFrameworkCore.Migrations;

namespace FormBuilderDemo.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form_tb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form_tb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormData_tb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormData_tb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormData_tb_Form_tb_FormId",
                        column: x => x.FormId,
                        principalTable: "Form_tb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormField_tb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    FieldType = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    MaxLength = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Options = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormField_tb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormField_tb_Form_tb_FormId",
                        column: x => x.FormId,
                        principalTable: "Form_tb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormData_tb_FormId",
                table: "FormData_tb",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormField_tb_FormId",
                table: "FormField_tb",
                column: "FormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormData_tb");

            migrationBuilder.DropTable(
                name: "FormField_tb");

            migrationBuilder.DropTable(
                name: "Form_tb");
        }
    }
}
