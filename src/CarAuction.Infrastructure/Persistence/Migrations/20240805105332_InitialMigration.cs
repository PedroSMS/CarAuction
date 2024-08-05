using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarAuction.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    StartingBid = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auction_Vehicle_CarId",
                        column: x => x.CarId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HatchBack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfDoors = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HatchBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HatchBack_Vehicle_Id",
                        column: x => x.Id,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sedan",
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
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suv",
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
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
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
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bid_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "Identifier", "Manufacturer", "Model", "StartingBid", "Year" },
                values: new object[,]
                {
                    { new Guid("061a854f-4901-4481-84e5-7607beb5017d"), "c4670c19-8e3c-4639-860a-c0000b8f1de3", "Polestar", "Camry", 15754m, 1978 },
                    { new Guid("0b904807-bd0d-4126-84a7-99d1d8e60cbc"), "ce9bf647-29b2-4dab-8880-ab45b5458b56", "Smart", "Model 3", 19944m, 2019 },
                    { new Guid("0c39ba4f-b3b3-4f89-b11d-026e19b7d02f"), "7a2a4257-8de5-46a7-8aaf-786e120a46e7", "Smart", "Corvette", 20434m, 1999 },
                    { new Guid("10bbdcf5-7d2a-4af6-8149-80ed4f965e0f"), "bd11b83b-880a-4f17-a2a6-90cc9665a531", "Land Rover", "1", 3146m, 2000 },
                    { new Guid("278a03fb-6e7d-4806-a75d-4270147d31e2"), "15f4aa9c-4c24-43ba-a60f-b2d2d85f675d", "Mini", "XTS", 5782m, 2019 },
                    { new Guid("32ac0182-15ab-4092-b97a-ecf6a47064cd"), "49e6fc8b-f535-4ce5-89a2-c5d34f692b9a", "Bentley", "Altima", 24685m, 1955 },
                    { new Guid("340d7218-aec1-4040-b78b-34c61804f2f4"), "6d6271ad-40b9-4951-9712-b4a69f85d0eb", "Volvo", "Countach", 20556m, 2009 },
                    { new Guid("3f7bb23a-58cf-4285-8a94-8dac0f6ca8d3"), "2520d40c-5d94-45df-82e2-3c89717208c3", "Volvo", "Civic", 23465m, 1982 },
                    { new Guid("43c0b8eb-b9ac-4b49-adb4-91d379ab593b"), "25ee1d62-fce9-4869-b7f3-8211274b5406", "Bugatti", "Element", 18079m, 1963 },
                    { new Guid("44a8d54f-52a5-4623-b20a-a2a4496bdd0c"), "51852392-4513-4a25-a08b-12662fdc6762", "Polestar", "Spyder", 21112m, 1950 },
                    { new Guid("48112217-933c-4d27-ad1d-56d7369f96c7"), "015212a3-5ca9-4983-b624-37597c1bf13a", "Chevrolet", "Altima", 7603m, 1997 },
                    { new Guid("59d89a8a-5a6a-4969-b1dd-bd6c5e6f4b16"), "63ccec42-6dc2-401b-b011-c658a1bc3dc5", "Chevrolet", "Jetta", 8041m, 2016 },
                    { new Guid("5f189beb-2bb2-4a65-91ee-5c3d0961d4ac"), "74e7b48e-95c9-4c21-bfdd-8bc8336fc843", "Aston Martin", "Grand Cherokee", 10870m, 1991 },
                    { new Guid("6d7da468-de46-4f5f-a1f5-70addf4f9c53"), "d2b3037f-b060-43f9-95bd-a96ff6fd7c1b", "Land Rover", "Roadster", 11151m, 2014 },
                    { new Guid("977145aa-2b9d-4aa0-a902-87c9d3d9e7f7"), "3c198e42-1522-4e4b-82a8-e90b58d56a8e", "Mazda", "Sentra", 4348m, 1962 },
                    { new Guid("97c5938f-61d8-41b4-971d-5fafa907e89d"), "31b0cd3a-bd31-4ba6-aebf-96a1793cd057", "Maserati", "Malibu", 19488m, 2018 },
                    { new Guid("985094bf-4684-4907-8877-dfd91e3f60b1"), "f2798f41-9595-48c8-abfd-471a39edf710", "Polestar", "Escalade", 5930m, 2012 },
                    { new Guid("a852a068-4630-4660-a1da-c69e833467dc"), "ce4e7aba-3ff9-4b5a-93d7-c378c37893d3", "Fiat", "A8", 2219m, 1987 },
                    { new Guid("ad786f35-2969-4837-8757-7466a699ab68"), "088cd0de-f351-4340-9d73-3684ed5c2b81", "Cadillac", "Charger", 7320m, 1970 },
                    { new Guid("b0820feb-e231-454b-acc8-9737f5b44f65"), "4140f652-14cb-4dbe-9ab2-529deb730bab", "Kia", "A8", 6367m, 1985 },
                    { new Guid("b48fbeb8-d435-4846-8fcd-28cd9af12db3"), "63adc9ae-eb8d-43e0-80d0-ef1775fe4112", "Nissan", "Expedition", 21704m, 2006 },
                    { new Guid("bcea2705-63bb-402c-8443-119a9d2a2c27"), "13433b57-0a13-4ff9-a0d7-3e6c79244a1b", "Volvo", "ATS", 16724m, 1974 },
                    { new Guid("bf6e74d7-a7c4-4181-84de-0aa90c19f0f8"), "d83e9d02-3b01-4c62-b1da-6fe8f19fc47f", "Fiat", "XC90", 16723m, 1974 },
                    { new Guid("c0dc4bf6-014f-4fef-8067-d7c2f3c45079"), "2f67f0d4-2442-486d-9573-82c0d452752f", "Mazda", "Aventador", 20473m, 1960 },
                    { new Guid("c38df5ac-acb0-4cd9-80f9-249464287d4a"), "6f52f2ce-be6d-4dc7-a826-0a5b06ce1cb0", "Smart", "Explorer", 16312m, 1984 },
                    { new Guid("c85ff316-8371-4f94-b12f-ee06646a37ea"), "224fbf99-1582-4b6c-9a0b-60393f679395", "Audi", "Impala", 18176m, 1968 },
                    { new Guid("d15d51dd-0f5b-4a6c-8391-a908647bd62e"), "9c320f5c-5961-4932-a178-95994fef0c85", "Dodge", "Cruze", 14007m, 1996 },
                    { new Guid("d2c820bb-abbd-4f10-83c4-41eb8ab4e29a"), "0b002f69-bb27-4f48-9fe6-3916703e597d", "Mercedes Benz", "A4", 24294m, 1965 },
                    { new Guid("d2de6c8e-0002-4807-8631-5fb53ddb67da"), "08edcf1b-6c53-49e1-b029-ad869f69f893", "Jaguar", "Golf", 22872m, 1996 },
                    { new Guid("d58f7970-dff5-405d-96e0-9fedc75f6ddc"), "542d3a18-2192-4fe5-8991-f872b8ccc07f", "Jaguar", "Prius", 15109m, 1967 },
                    { new Guid("d80e515d-3f39-4783-8894-3909e603dc64"), "ff63517f-c3c5-4d56-9922-338256ea6f52", "Bugatti", "A4", 20108m, 2015 },
                    { new Guid("dc227f9f-1b8b-4184-a544-8c1b52b66c38"), "25cfe4b9-bc75-4409-b36d-1783a9fb0ca9", "Land Rover", "Charger", 16552m, 1964 },
                    { new Guid("dcdc50b6-7b46-46b4-b659-f02e865f8bf0"), "4429f1dc-1113-4ae2-ab4a-fe0a2f92a450", "Lamborghini", "Ranchero", 7109m, 2000 },
                    { new Guid("e4242445-271f-41c9-866b-a06cc9d350e3"), "06084231-62ec-4324-b594-06450705137f", "Polestar", "Camaro", 7738m, 1995 },
                    { new Guid("ee5e89dd-4ab1-4ee7-9f03-7398a8b61479"), "f8d3bfcf-f376-44ea-86c8-46d929b002fe", "Tesla", "Civic", 9882m, 1958 },
                    { new Guid("ef16e374-4794-4d9b-a2a0-09ce446b8b54"), "567df195-ad1f-45ff-ba27-d6a91ce9cec9", "Maserati", "Model S", 14224m, 1957 },
                    { new Guid("ef3424eb-87ef-44bb-8e10-d55849794d95"), "3ee10255-1ee5-4eec-8322-5b594236e361", "Audi", "Durango", 20194m, 2014 },
                    { new Guid("f0db6a0d-6476-4294-bd1a-9ff180eba012"), "9dd44840-daa2-4771-84ce-328e278f9e13", "Chevrolet", "Durango", 23336m, 1999 },
                    { new Guid("fa22da69-5d54-4b39-b3df-61ca1f592e45"), "38a27097-3d02-4c60-9178-ee96d435e0f6", "Fiat", "Silverado", 6986m, 1988 },
                    { new Guid("fe9a49de-9442-4c57-b3c2-28665b05c975"), "b4c88a38-2fdc-4786-9d5a-23afd4c55d6e", "BMW", "Mercielago", 4658m, 2020 }
                });

            migrationBuilder.InsertData(
                table: "HatchBack",
                columns: new[] { "Id", "NumberOfDoors" },
                values: new object[,]
                {
                    { new Guid("278a03fb-6e7d-4806-a75d-4270147d31e2"), 1 },
                    { new Guid("340d7218-aec1-4040-b78b-34c61804f2f4"), 4 },
                    { new Guid("48112217-933c-4d27-ad1d-56d7369f96c7"), 1 },
                    { new Guid("6d7da468-de46-4f5f-a1f5-70addf4f9c53"), 3 },
                    { new Guid("977145aa-2b9d-4aa0-a902-87c9d3d9e7f7"), 5 },
                    { new Guid("a852a068-4630-4660-a1da-c69e833467dc"), 2 },
                    { new Guid("d2de6c8e-0002-4807-8631-5fb53ddb67da"), 5 },
                    { new Guid("dcdc50b6-7b46-46b4-b659-f02e865f8bf0"), 4 },
                    { new Guid("ef16e374-4794-4d9b-a2a0-09ce446b8b54"), 3 },
                    { new Guid("fa22da69-5d54-4b39-b3df-61ca1f592e45"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Sedan",
                columns: new[] { "Id", "NumberOfDoors" },
                values: new object[,]
                {
                    { new Guid("0c39ba4f-b3b3-4f89-b11d-026e19b7d02f"), 3 },
                    { new Guid("32ac0182-15ab-4092-b97a-ecf6a47064cd"), 5 },
                    { new Guid("3f7bb23a-58cf-4285-8a94-8dac0f6ca8d3"), 5 },
                    { new Guid("44a8d54f-52a5-4623-b20a-a2a4496bdd0c"), 1 },
                    { new Guid("97c5938f-61d8-41b4-971d-5fafa907e89d"), 4 },
                    { new Guid("b48fbeb8-d435-4846-8fcd-28cd9af12db3"), 3 },
                    { new Guid("c0dc4bf6-014f-4fef-8067-d7c2f3c45079"), 2 },
                    { new Guid("d15d51dd-0f5b-4a6c-8391-a908647bd62e"), 2 },
                    { new Guid("d2c820bb-abbd-4f10-83c4-41eb8ab4e29a"), 3 },
                    { new Guid("ee5e89dd-4ab1-4ee7-9f03-7398a8b61479"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Suv",
                columns: new[] { "Id", "NumberOfSeats" },
                values: new object[,]
                {
                    { new Guid("10bbdcf5-7d2a-4af6-8149-80ed4f965e0f"), 7 },
                    { new Guid("43c0b8eb-b9ac-4b49-adb4-91d379ab593b"), 9 },
                    { new Guid("59d89a8a-5a6a-4969-b1dd-bd6c5e6f4b16"), 6 },
                    { new Guid("5f189beb-2bb2-4a65-91ee-5c3d0961d4ac"), 7 },
                    { new Guid("bcea2705-63bb-402c-8443-119a9d2a2c27"), 8 },
                    { new Guid("bf6e74d7-a7c4-4181-84de-0aa90c19f0f8"), 6 },
                    { new Guid("c38df5ac-acb0-4cd9-80f9-249464287d4a"), 5 },
                    { new Guid("e4242445-271f-41c9-866b-a06cc9d350e3"), 6 },
                    { new Guid("f0db6a0d-6476-4294-bd1a-9ff180eba012"), 6 },
                    { new Guid("fe9a49de-9442-4c57-b3c2-28665b05c975"), 8 }
                });

            migrationBuilder.InsertData(
                table: "Truck",
                columns: new[] { "Id", "LoadCapacity" },
                values: new object[,]
                {
                    { new Guid("061a854f-4901-4481-84e5-7607beb5017d"), 10889 },
                    { new Guid("0b904807-bd0d-4126-84a7-99d1d8e60cbc"), 27772 },
                    { new Guid("985094bf-4684-4907-8877-dfd91e3f60b1"), 23456 },
                    { new Guid("ad786f35-2969-4837-8757-7466a699ab68"), 23021 },
                    { new Guid("b0820feb-e231-454b-acc8-9737f5b44f65"), 31299 },
                    { new Guid("c85ff316-8371-4f94-b12f-ee06646a37ea"), 23320 },
                    { new Guid("d58f7970-dff5-405d-96e0-9fedc75f6ddc"), 12264 },
                    { new Guid("d80e515d-3f39-4783-8894-3909e603dc64"), 10925 },
                    { new Guid("dc227f9f-1b8b-4184-a544-8c1b52b66c38"), 32911 },
                    { new Guid("ef3424eb-87ef-44bb-8e10-d55849794d95"), 16292 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CarId",
                table: "Auction",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_AuctionId",
                table: "Bid",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Identifier",
                table: "Vehicle",
                column: "Identifier",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "HatchBack");

            migrationBuilder.DropTable(
                name: "Sedan");

            migrationBuilder.DropTable(
                name: "Suv");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "Auction");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
