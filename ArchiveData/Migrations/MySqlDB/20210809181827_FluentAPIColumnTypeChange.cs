using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations.MySqlDB
{
    public partial class FluentAPIColumnTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InputNotificationEventDefinitionEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventDefinitionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventTargetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    ClientId = table.Column<string>(type: "varchar(36)", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InputNotificationEventEntities",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventDefinitionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventTargetId = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    ClientId = table.Column<string>(type: "varchar(36)", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
