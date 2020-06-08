using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarCommander.Migrations.Message
{
	public partial class AddMessages : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"Commands",
				table => new
				{
					Id = table.Column<Guid>(),
					Json = table.Column<string>(),
					Created = table.Column<DateTimeOffset>(),
					Processed = table.Column<DateTimeOffset>(nullable: true),
					ScheduledFor = table.Column<DateTimeOffset>(nullable: true),
					TargetId = table.Column<Guid>()
				},
				constraints: table => { table.PrimaryKey("PK_Commands", x => x.Id); });

			migrationBuilder.CreateTable(
				"Events",
				table => new
				{
					Id = table.Column<Guid>(),
					Json = table.Column<string>(),
					Created = table.Column<DateTimeOffset>(),
					Processed = table.Column<DateTimeOffset>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_Events", x => x.Id); });

			migrationBuilder.CreateTable(
				"Jobs",
				table => new
				{
					Id = table.Column<Guid>(),
					QueueId = table.Column<Guid>(),
					MessageId = table.Column<Guid>(),
					Address = table.Column<string>(),
					Handler = table.Column<string>(),
					Created = table.Column<DateTimeOffset>()
				},
				constraints: table => { table.PrimaryKey("PK_Jobs", x => x.Id); });

			migrationBuilder.CreateIndex(
				"IX_Commands_Created",
				"Commands",
				"Created");

			migrationBuilder.CreateIndex(
				"IX_Commands_Processed",
				"Commands",
				"Processed");

			migrationBuilder.CreateIndex(
				"IX_Commands_TargetId",
				"Commands",
				"TargetId");

			migrationBuilder.CreateIndex(
				"IX_Events_Created",
				"Events",
				"Created");

			migrationBuilder.CreateIndex(
				"IX_Events_Processed",
				"Events",
				"Processed");

			migrationBuilder.CreateIndex(
				"IX_Jobs_MessageId",
				"Jobs",
				"MessageId");

			migrationBuilder.CreateIndex(
				"IX_Jobs_QueueId",
				"Jobs",
				"QueueId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"Commands");

			migrationBuilder.DropTable(
				"Events");

			migrationBuilder.DropTable(
				"Jobs");
		}
	}
}