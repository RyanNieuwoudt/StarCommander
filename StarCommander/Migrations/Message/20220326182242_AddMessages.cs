using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace StarCommander.Migrations.Message
{
    public partial class AddMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
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
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Processed = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    Json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QueueId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Handler = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_Created",
                table: "Commands",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_Processed",
                table: "Commands",
                column: "Processed");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_TargetId",
                table: "Commands",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Created",
                table: "Events",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Processed",
                table: "Events",
                column: "Processed");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_MessageId",
                table: "Jobs",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QueueId",
                table: "Jobs",
                column: "QueueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
