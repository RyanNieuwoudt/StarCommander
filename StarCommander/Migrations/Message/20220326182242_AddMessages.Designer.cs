﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;

#nullable disable

namespace StarCommander.Migrations.Message
{
    [DbContext(typeof(MessageDataContext))]
    [Migration("20220326182242_AddMessages")]
    partial class AddMessages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StarCommander.Infrastructure.Persistence.Aggregate.Messages.Command", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant?>("Processed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Instant?>("ScheduledFor")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Processed");

                    b.HasIndex("TargetId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("StarCommander.Infrastructure.Persistence.Aggregate.Messages.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant?>("Processed")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Created");

                    b.HasIndex("Processed");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("StarCommander.Infrastructure.Persistence.Aggregate.Messages.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Handler")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QueueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("QueueId");

                    b.ToTable("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}