using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StarCommander.Migrations.Projection
{
    public partial class AddProjections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipLocations",
                columns: table => new
                {
                    ShipLocationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShipId = table.Column<Guid>(type: "uuid", nullable: false),
                    Heading = table.Column<double>(type: "double precision", nullable: false),
                    Speed = table.Column<long>(type: "bigint", nullable: false),
                    X = table.Column<long>(type: "bigint", nullable: false),
                    Y = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipLocations", x => x.ShipLocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipLocations_X",
                table: "ShipLocations",
                column: "X");

            migrationBuilder.CreateIndex(
                name: "IX_ShipLocations_Y",
                table: "ShipLocations",
                column: "Y");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipLocations");
        }
    }
}
