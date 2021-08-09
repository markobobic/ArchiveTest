﻿// <auto-generated />
using System;
using ArchiveData.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArchiveData.Migrations.MySqlDB
{
    [DbContext(typeof(MySqlDBContext))]
    partial class MySqlDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ArchiveData.Model.ArchivedInputNotification", b =>
                {
                    b.Property<string>("EventId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("AcknowledgmentTimeStampUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("EventDefinitionId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventTargetId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("SourceEventTimeStampUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EventId");

                    b.HasIndex("EventDefinitionId");

                    b.ToTable("ArchivedInputNotifications");
                });

            modelBuilder.Entity("ArchiveData.Model.InputNotificationEventDefinitionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TermType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("InputNotificationEventDefinitionEntities");
                });

            modelBuilder.Entity("ArchiveData.Model.InputNotificationEventEntity", b =>
                {
                    b.Property<string>("EventId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("AcknowledgmentTimeStampUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ClientId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("EventDefinitionId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventTargetId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("SourceEventTimeStampUtc")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EventId");

                    b.HasIndex("EventDefinitionId");

                    b.HasIndex(new[] { "SourceEventTimeStampUtc" }, "IX_SourceEventTimeStampUTC");

                    b.ToTable("InputNotificationEventEntities");
                });

            modelBuilder.Entity("ArchiveData.Model.ArchivedInputNotification", b =>
                {
                    b.HasOne("ArchiveData.Model.InputNotificationEventDefinitionEntity", "EventDefinition")
                        .WithMany()
                        .HasForeignKey("EventDefinitionId");

                    b.Navigation("EventDefinition");
                });

            modelBuilder.Entity("ArchiveData.Model.InputNotificationEventEntity", b =>
                {
                    b.HasOne("ArchiveData.Model.InputNotificationEventDefinitionEntity", "EventDefinition")
                        .WithMany()
                        .HasForeignKey("EventDefinitionId");

                    b.Navigation("EventDefinition");
                });
#pragma warning restore 612, 618
        }
    }
}
