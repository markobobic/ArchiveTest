using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations
{
    public partial class ChangeInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputNotificationEventDefinitionEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventDefinitionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedInputNotifications",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EventDefinitionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventTargetId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedInputNotifications", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_ArchivedInputNotifications_InputNotificationEventDefinitionEntities_EventDefinitionId",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InputNotificationEventEntities",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EventDefinitionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventTargetId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventEntities", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_InputNotificationEventEntities_InputNotificationEventDefinitionEntities_EventDefinitionId",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
