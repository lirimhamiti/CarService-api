using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGarageCarsAndMakeCarsGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_GarageId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "GarageId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Cars",
                type: "character(17)",
                fixedLength: true,
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.CreateTable(
                name: "GarageCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GarageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarageCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarageCars_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Vin",
                table: "Cars",
                column: "Vin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarageCars_CarId",
                table: "GarageCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_GarageCars_GarageId_CarId",
                table: "GarageCars",
                columns: new[] { "GarageId", "CarId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarageCars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_Vin",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Cars",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(17)",
                oldFixedLength: true,
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<Guid>(
                name: "GarageId",
                table: "Cars",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cars_GarageId",
                table: "Cars",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Garages_GarageId",
                table: "Cars",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
