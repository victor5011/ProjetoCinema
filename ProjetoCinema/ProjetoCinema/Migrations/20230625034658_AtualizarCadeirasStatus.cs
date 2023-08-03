using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCinema.Migrations
{
    public partial class AtualizarCadeirasStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Cadeiras",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cadeiras");
        }
    }
}
