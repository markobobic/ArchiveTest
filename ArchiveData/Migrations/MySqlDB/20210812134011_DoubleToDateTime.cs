using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveData.Migrations.MySqlDB
{
    public partial class DoubleToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "InputNotificationEventEntities",
                type: "double",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "SourceEventTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");

            migrationBuilder.AlterColumn<double>(
                name: "AcknowledgmentTimeStampUtc",
                table: "ArchivedInputNotifications",
                type: "double",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP");
        }
    }
}
