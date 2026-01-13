using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ServiceRecords",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_GarageId_CarId",
                table: "ServiceRecords",
                columns: new[] { "GarageId", "CarId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_ServiceDate",
                table: "ServiceRecords",
                column: "ServiceDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceRecords_GarageId_CarId",
                table: "ServiceRecords");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRecords_ServiceDate",
                table: "ServiceRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ServiceRecords",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
