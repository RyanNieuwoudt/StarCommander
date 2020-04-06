using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarCommander.Migrations.Player
{
	public partial class AddPlayers : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"Players",
				table => new
				{
					Id = table.Column<Guid>(),
					Json = table.Column<string>(),
					CallSign = table.Column<string>()
				},
				constraints: table => { table.PrimaryKey("PK_Players", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_Players_CallSign",
				"Players",
				"CallSign",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"Players");
		}
	}
}