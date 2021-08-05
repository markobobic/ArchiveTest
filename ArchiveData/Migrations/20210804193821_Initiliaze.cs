using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations
{
    public partial class Initiliaze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputNotificationEventDefinitionEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventDefinitionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputNotificationEventEntity",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTargetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceEventTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcknowledgmentTimeStampUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputNotificationEventEntity", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_InputNotificationEventEntity_InputNotificationEventDefinitionEntities_EventDefinitionId",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputNotificationEventEntity_EventDefinitionId",
                table: "InputNotificationEventEntity",
                column: "EventDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputNotificationEventEntity");

            migrationBuilder.DropTable(
                name: "InputNotificationEventDefinitionEntities");
        }
    }
}
