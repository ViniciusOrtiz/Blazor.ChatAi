using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_DocumentEntity_AddFileColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "App",
                table: "Documents",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                schema: "App",
                table: "Documents",
                type: "BYTEA",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInBytes",
                schema: "App",
                table: "Documents",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "App",
                table: "Documents",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                schema: "App",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileSizeInBytes",
                schema: "App",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "App",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "App",
                table: "Documents",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
