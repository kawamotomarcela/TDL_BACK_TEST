using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDLembretes.Migrations
{
    /// <inheritdoc />
    public partial class marcelaotest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlarmeAtivado",
                table: "TarefasPersonalizada",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmeAtivado",
                table: "TarefasPersonalizada");
        }
    }
}
