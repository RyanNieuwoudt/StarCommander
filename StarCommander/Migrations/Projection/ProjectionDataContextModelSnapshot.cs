﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StarCommander.Infrastructure.Persistence.Projection;

namespace StarCommander.Migrations.Projection
{
	[DbContext(typeof(ProjectionDataContext))]
	class ProjectionDataContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.HasAnnotation("ProductVersion", "3.1.3")
				.HasAnnotation("Relational:MaxIdentifierLength", 63);

			modelBuilder.Entity("StarCommander.Infrastructure.Persistence.Projection.ShipLocations.ShipLocation", b =>
			{
				b.Property<long>("ShipLocationId")
					.ValueGeneratedOnAdd()
					.HasColumnType("bigint")
					.HasAnnotation("Npgsql:ValueGenerationStrategy",
						NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

				b.Property<DateTimeOffset>("Created")
					.HasColumnType("timestamp with time zone");

				b.Property<double>("Heading")
					.HasColumnType("double precision");

				b.Property<Guid>("ShipId")
					.HasColumnType("uuid");

				b.Property<long>("Speed")
					.HasColumnType("bigint");

				b.Property<long>("X")
					.HasColumnType("bigint");

				b.Property<long>("Y")
					.HasColumnType("bigint");

				b.HasKey("ShipLocationId");

				b.HasIndex("X");

				b.HasIndex("Y");

				b.ToTable("ShipLocations");
			});
#pragma warning restore 612, 618
		}
	}
}