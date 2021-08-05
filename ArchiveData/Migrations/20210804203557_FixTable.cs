using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations
{
    public partial class FixTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InputNotificationEventEntities");

            migrationBuilder.CreateTable(
                name: "ArchivedInputNotifications",
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
                    table.PrimaryKey("PK_ArchivedInputNotifications", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_ArchivedInputNotifications_InputNotificationEventDefinitionEntities_EventDefinitionId",
                        column: x => x.EventDefinitionId,
                        principalTable: "InputNotificationEventDefinitionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedInputNotifications_EventDefinitionId",
                table: "ArchivedInputNotifications",
                column: "EventDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedInputNotifications");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InputNotificationEventEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
