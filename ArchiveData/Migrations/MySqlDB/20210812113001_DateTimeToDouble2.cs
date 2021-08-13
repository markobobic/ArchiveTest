using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations.MySqlDB
{
    public partial class DateTimeToDouble2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
