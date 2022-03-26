using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace StarCommander.Migrations.Ship
{
    public partial class AddShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipCommands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Processed = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    ScheduledFor = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipCommands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipCommands_Created",
                table: "ShipCommands",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ShipCommands_Processed",
                table: "ShipCommands",
                column: "Processed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipCommands");
        }
    }
}
