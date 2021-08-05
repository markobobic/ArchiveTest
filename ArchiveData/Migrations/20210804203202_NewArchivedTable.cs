using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations
{
    public partial class NewArchivedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputNotificationEventEntity_InputNotificationEventDefinitionEntities_EventDefinitionId",
                table: "InputNotificationEventEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InputNotificationEventEntity",
                table: "InputNotificationEventEntity");

            migrationBuilder.RenameTable(
                name: "InputNotificationEventEntity",
                newName: "InputNotificationEventEntities");

            migrationBuilder.RenameIndex(
                name: "IX_InputNotificationEventEntity_EventDefinitionId",
                table: "InputNotificationEventEntities",
                newName: "IX_InputNotificationEventEntities_EventDefinitionId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InputNotificationEventEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputNotificationEventEntities",
                table: "InputNotificationEventEntities",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_InputNotificationEventEntities_InputNotificationEventDefinitionEntities_EventDefinitionId",
                table: "InputNotificationEventEntities",
                column: "EventDefinitionId",
                principalTable: "InputNotificationEventDefinitionEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputNotificationEventEntities_InputNotificationEventDefinitionEntities_EventDefinitionId",
                table: "InputNotificationEventEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InputNotificationEventEntities",
                table: "InputNotificationEventEntities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InputNotificationEventEntities");

            migrationBuilder.RenameTable(
                name: "InputNotificationEventEntities",
                newName: "InputNotificationEventEntity");

            migrationBuilder.RenameIndex(
                name: "IX_InputNotificationEventEntities_EventDefinitionId",
                table: "InputNotificationEventEntity",
                newName: "IX_InputNotificationEventEntity_EventDefinitionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputNotificationEventEntity",
                table: "InputNotificationEventEntity",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_InputNotificationEventEntity_InputNotificationEventDefinitionEntities_EventDefinitionId",
                table: "InputNotificationEventEntity",
                column: "EventDefinitionId",
                principalTable: "InputNotificationEventDefinitionEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
