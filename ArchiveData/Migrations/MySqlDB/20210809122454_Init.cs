using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations.MySqlDB
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InputNotificationEventDefinitionEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TermType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventDefinitionEntities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArchivedInputNotifications",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDefinitionId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventTargetId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClientId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedInputNotifications", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_ArchivedInputNotifications_InputNotificationEventDefinitionE~",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InputNotificationEventEntities",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventDefinitionId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventTargetId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClientId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventEntities", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_InputNotificationEventEntities_InputNotificationEventDefinit~",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedInputNotifications_EventDefinitionId",
                table: "ArchivedInputNotifications",
                column: "EventDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_InputNotificationEventEntities_EventDefinitionId",
                table: "InputNotificationEventEntities",
                column: "EventDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceEventTimeStampUTC",
                table: "InputNotificationEventEntities",
                column: "SourceEventTimeStampUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedInputNotifications");

            migrationBuilder.DropTable(
                name: "InputNotificationEventEntities");

            migrationBuilder.DropTable(
                name: "InputNotificationEventDefinitionEntities");
        }
    }
}
