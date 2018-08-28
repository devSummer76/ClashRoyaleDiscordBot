using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    ClanTag = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.ClanTag);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerTag = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ClanTag = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerTag);
                    table.ForeignKey(
                        name: "FK_Players_Clans_ClanTag",
                        column: x => x.ClanTag,
                        principalTable: "Clans",
                        principalColumn: "ClanTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClanTag",
                table: "Players",
                column: "ClanTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Clans");
        }
    }
}
