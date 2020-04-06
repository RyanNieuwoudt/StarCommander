using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarCommander.Migrations.Ship
{
	public partial class AddShips : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"ShipCommands",
				table => new
				{
					Id = table.Column<Guid>(),
					Json = table.Column<string>(),
					Created = table.Column<DateTimeOffset>(),
					Processed = table.Column<DateTimeOffset>(nullable: true),
					ScheduledFor = table.Column<DateTimeOffset>(nullable: true),
					TargetId = table.Column<Guid>()
				},
				constraints: table => { table.PrimaryKey("PK_ShipCommands", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_ShipCommands_Created",
				"ShipCommands",
				"Created");

			migrationBuilder.CreateIndex(
				"IX_ShipCommands_Processed",
				"ShipCommands",
				"Processed");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"ShipCommands");
		}
	}
}