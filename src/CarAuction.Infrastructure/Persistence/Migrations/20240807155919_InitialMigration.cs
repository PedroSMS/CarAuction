using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auction");

            migrationBuilder.CreateTable(
                name: "Vehicle",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    OpeningBid = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auction",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auction_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "auction",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hatchback",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfDoors = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hatchback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hatchback_Vehicle_Id",
                        column: x => x.Id,
                        principalSchema: "auction",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedan",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfDoors = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedan_Vehicle_Id",
                        column: x => x.Id,
                        principalSchema: "auction",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suv",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suv_Vehicle_Id",
                        column: x => x.Id,
                        principalSchema: "auction",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoadCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Truck_Vehicle_Id",
                        column: x => x.Id,
                        principalSchema: "auction",
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                schema: "auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    PlacedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bid_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalSchema: "auction",
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_VehicleId",
                schema: "auction",
                table: "Auction",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_AuctionId_Value",
                schema: "auction",
                table: "Bid",
                columns: new[] { "AuctionId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Identifier",
                schema: "auction",
                table: "Vehicle",
                column: "Identifier",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bid",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Hatchback",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Sedan",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Suv",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Truck",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Auction",
                schema: "auction");

            migrationBuilder.DropTable(
                name: "Vehicle",
                schema: "auction");
        }
    }
}
