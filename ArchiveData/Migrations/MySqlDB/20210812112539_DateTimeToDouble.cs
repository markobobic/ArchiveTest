using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations.MySqlDB
{
    public partial class DateTimeToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double(10,1)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(10,1)");
        }
    }
}
