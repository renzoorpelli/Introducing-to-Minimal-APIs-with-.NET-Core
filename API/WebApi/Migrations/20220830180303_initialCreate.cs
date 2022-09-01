using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fabricante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabricante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cerveza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FabricanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cerveza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cerveza_Fabricante_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "Fabricante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fabricante",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "Quilmes" });

            migrationBuilder.InsertData(
                table: "Fabricante",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 2, "Patagonia" });

            migrationBuilder.InsertData(
                table: "Cerveza",
                columns: new[] { "Id", "Descripcion", "FabricanteId", "Nombre" },
                values: new object[] { 1, "Cerveza más elegida desde 1890.", 1, "Classic" });

            migrationBuilder.InsertData(
                table: "Cerveza",
                columns: new[] { "Id", "Descripcion", "FabricanteId", "Nombre" },
                values: new object[] { 2, "Cerveza Ipa fabricanda en el sur de la Patagonia Argentina.", 2, "IPA" });

            migrationBuilder.CreateIndex(
                name: "IX_Cerveza_FabricanteId",
                table: "Cerveza",
                column: "FabricanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cerveza");

            migrationBuilder.DropTable(
                name: "Fabricante");
        }
    }
}
