using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StarCommander.Migrations.Projection;

public partial class AddProjections : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"ShipLocations",
			table => new
			{
				ShipLocationId = table.Column<long>()
					.Annotation("Npgsql:ValueGenerationStrategy",
						NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				ShipId = table.Column<Guid>(),
				Heading = table.Column<double>(),
				Speed = table.Column<long>(),
				X = table.Column<long>(),
				Y = table.Column<long>(),
				Created = table.Column<DateTimeOffset>()
			},
			constraints: table => { table.PrimaryKey("PK_ShipLocations", x => x.ShipLocationId); });

		migrationBuilder.CreateIndex(
			"IX_ShipLocations_X",
			"ShipLocations",
			"X");

		migrationBuilder.CreateIndex(
			"IX_ShipLocations_Y",
			"ShipLocations",
			"Y");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"ShipLocations");
	}
}