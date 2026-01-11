using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGarageAuthAndApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Cars");

            migrationBuilder.AddColumn<Guid>(
                name: "GarageId",
                table: "ServiceRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ServiceRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Garages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Garages",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Garages",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Garages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Garages",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Garages_Email",
                table: "Garages",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Garages_Username",
                table: "Garages",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Garages_Email",
                table: "Garages");

            migrationBuilder.DropIndex(
                name: "IX_Garages_Username",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "GarageId",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Garages");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Cars",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
