using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarAuction.Infrastructure.Migrations
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
                columns: new[] { "Id", "Identifier", "Manufacturer", "StartingBid", "Year" },
                values: new object[,]
                {
                    { new Guid("009daeb8-a2ef-4a5f-a65e-15223dcc9f73"), "130bac51-8928-4a0d-b22e-bb092fd1fa50", "Mercedes Benz", 17819m, 2022 },
                    { new Guid("0313a818-eac2-497b-b7bd-83c9881c1fa7"), "cb1fa826-e446-4313-9585-b64690ed7ecf", "Aston Martin", 22833m, 1955 },
                    { new Guid("03948eae-630a-4ec6-9d9c-353314ce2f66"), "bbd6d4ad-4637-465a-90f2-6e64dedb91a4", "Mazda", 6519m, 2010 },
                    { new Guid("044f1e5a-037e-4f68-bcdf-069daae12594"), "c266654d-f907-4eab-8c05-1630d45315e8", "Kia", 1695m, 1951 },
                    { new Guid("045e152a-1ee3-459b-a2eb-f2513b89af2d"), "6e0f7128-f4b7-4375-b736-7caaa57ef1af", "Mini", 4206m, 1950 },
                    { new Guid("05e166c7-583e-4d63-8f80-c65f66cbf775"), "be617e67-d77c-4c32-9f3a-def70f22d3bb", "Nissan", 6755m, 1990 },
                    { new Guid("0738449b-28e6-4d74-bc27-2cb2744c5a87"), "b9ef7ed7-6383-4422-9b24-5c4afe884cb6", "BMW", 5149m, 2012 },
                    { new Guid("0a2b3679-8764-46b2-85ac-31f2a7efa862"), "53da6e84-7c0f-4e4b-be7a-8f64095f32e1", "Porsche", 14945m, 1971 },
                    { new Guid("0baaa524-da79-434d-ba2d-66b337926401"), "0fdec874-d51f-439a-a26a-a5657ee99109", "Mazda", 6717m, 1988 },
                    { new Guid("0bd54979-24bf-4075-bb13-2f1152cd9319"), "399ce515-746d-4d9e-b26f-731f16751900", "Maserati", 2268m, 2006 },
                    { new Guid("0de917f8-7db4-40aa-b7f4-3e56ff3baee9"), "0b7d9ffd-48a6-43dd-b990-0fc1410864a8", "Volvo", 5389m, 1968 },
                    { new Guid("0eb0eb30-1b45-484b-a907-2725d8c57d0a"), "708250f9-fd9d-47ae-a9ab-59f9212f651a", "Polestar", 2619m, 1985 },
                    { new Guid("0edd857a-2f94-4e8f-8f48-bc2f851b5f51"), "6310d27b-09a5-4253-8e0b-3050e47692c2", "Audi", 16560m, 1983 },
                    { new Guid("0f8c2fa3-e658-467e-aef9-65258c184378"), "791a8804-97f5-4a38-831d-0851f278ea8e", "Chevrolet", 20961m, 1953 },
                    { new Guid("1365d434-3509-4072-80b0-0a31bd80e6c2"), "3de2de03-bc1d-4c72-9d84-9e6260fac5a6", "Mercedes Benz", 16419m, 2023 },
                    { new Guid("137a4a7a-c6b5-47c0-9e9b-a4e289df5c62"), "f8dc9bd2-8eb5-46ce-be2b-0b4497544284", "Jeep", 1775m, 2005 },
                    { new Guid("17536aeb-1bc1-4e7b-8b56-a357e8e49d43"), "7617c830-ccdf-402e-9abd-2e431a1eb31a", "Maserati", 17960m, 1991 },
                    { new Guid("19046a1f-99ac-4225-928b-7680a96c5eb9"), "7e66e591-6bc2-483e-bb1a-f53cb8fc01c1", "Bugatti", 5257m, 1976 },
                    { new Guid("197c99d5-e25b-458b-af8d-761e81d7a830"), "de2021f1-bce4-4712-a6bc-6b38d06bfb74", "Volkswagen", 19538m, 2003 },
                    { new Guid("19e197ee-bfdd-4054-9cb0-611f05810613"), "b87fc9d4-68a3-4938-93fd-32b9ab1fa3a1", "BMW", 4424m, 1994 },
                    { new Guid("1a090a94-f09c-4a0e-a479-fc36c592ad7a"), "72b322f6-50b1-49c0-8079-214cc80baefa", "Kia", 6617m, 1981 },
                    { new Guid("1a16c35c-3277-4e83-b0ad-f776947d0a35"), "9947614a-bc6d-4dac-ad4a-6213cd53ec97", "Land Rover", 2590m, 2024 },
                    { new Guid("1ca7119b-7d8d-4cf2-ab57-77c062e33403"), "ad5cd7a5-a941-47c4-94cd-452b38ae1bc9", "Volvo", 14625m, 1989 },
                    { new Guid("1d66f16a-df11-4069-a591-4bca9211c702"), "9d49f426-81e2-4496-81a5-af0e994bfc24", "BMW", 13854m, 1953 },
                    { new Guid("1e5debd4-b592-4380-b5d4-47c200a91e36"), "ab62eaf8-6484-453b-a2fa-a41c4a7e1527", "Hyundai", 16516m, 2016 },
                    { new Guid("1f10e3a3-0036-416a-a700-85560126f98d"), "5e46b134-3049-439d-a615-f54cbcec288d", "Kia", 24754m, 1980 },
                    { new Guid("1f640420-e15c-456a-b24a-8972a38f78db"), "f931c46e-70d2-47e6-ac58-726f010edf80", "Chrysler", 10469m, 1954 },
                    { new Guid("21eee76f-8e0d-42f8-9538-21e1ed90a81a"), "1121562e-79f2-49be-95c9-9403dfbb2e7d", "Volvo", 14585m, 1958 },
                    { new Guid("27e7b2fb-5c76-4ee5-a643-971be2127bf7"), "a04f7a43-823f-462d-94a3-ac1c68c32e06", "Fiat", 6774m, 1965 },
                    { new Guid("288d541a-f698-467a-9039-89066f4135ff"), "2c9291af-87a0-424e-99a7-4001e64bd329", "Audi", 9856m, 1980 },
                    { new Guid("28c76101-741b-4c0b-9a9e-4f50bb4538aa"), "3ba9a07b-b441-4900-ab88-5250d3adf734", "Mercedes Benz", 14356m, 1958 },
                    { new Guid("28edf843-143d-49b6-8d8e-1795afd7269f"), "1b52a959-7ba8-4374-a48b-96379871ac8f", "Cadillac", 23457m, 1996 },
                    { new Guid("2a9a73f5-c374-4635-a887-a9b24eb2cd63"), "77ff9df5-ac95-4c64-8d55-ebdc22c63cf4", "Maserati", 5116m, 2006 },
                    { new Guid("2b0198b6-121b-4c94-903c-2eefc0d733a1"), "6f838eea-afe6-4ac9-95c0-add304f6dafb", "Toyota", 21311m, 2001 },
                    { new Guid("2b22aa11-ebf0-464c-be6b-0c7cfb0c8364"), "c78a8292-dec0-44bc-9631-794f662a27f9", "BMW", 20486m, 1984 },
                    { new Guid("2d9cefe6-3cf2-477d-bba5-70c1f7d903d6"), "bfc54a0d-6d4a-4755-9370-50358c31e66a", "Polestar", 15320m, 2004 },
                    { new Guid("2e95018a-3f30-4829-9074-8f184cc18c13"), "aa543403-9fbd-4f6b-8a59-77686506d1c4", "Volkswagen", 24787m, 2003 },
                    { new Guid("2ed1411d-6900-4707-90db-27fee490b6e3"), "58fddc4e-5f7d-4dca-93ae-8ed2e856cc7b", "Cadillac", 5921m, 1960 },
                    { new Guid("2ee6efbe-4b43-495c-9276-0240c9a60acc"), "d29618d6-d441-4ff1-b00f-a2af95748160", "Hyundai", 8260m, 1954 },
                    { new Guid("2f7036db-bc1c-4dde-b3d4-712643493294"), "7a6280f2-b5b3-4965-9449-b660f6b772cc", "Jeep", 23447m, 1978 },
                    { new Guid("30ac5435-a5c4-4b97-afcd-bf0407cf60a8"), "6d93acfd-10d2-469c-a067-763b7585c570", "Smart", 11087m, 1994 },
                    { new Guid("32135ae4-ee23-4ec6-a8df-457ad50303be"), "5d58f1d3-cd35-438a-865b-ab84a387f7fc", "Bugatti", 8473m, 1975 },
                    { new Guid("339f7753-ff75-4fb5-a095-7514ef872ebd"), "7d86944e-9e20-4845-95ff-d6c1c13a9eaf", "Mazda", 4319m, 1992 },
                    { new Guid("371c02d8-8f34-43ee-b0c5-3052ae70b583"), "fc176b67-dc81-4e7e-be68-f72b08a64cf0", "BMW", 11001m, 2016 },
                    { new Guid("380c021d-596c-406d-a276-d56f6f41ad94"), "fb78486b-fde0-4f80-83da-61085b543db3", "Chrysler", 5946m, 2024 },
                    { new Guid("38ae2ebb-d80f-48e4-81d4-21f20df776ad"), "7e6f9bf8-c417-44fb-82ac-a386c1a5483a", "Volvo", 14980m, 1956 },
                    { new Guid("3a133a3a-e05b-47d0-ab37-9bf703dae72d"), "ae64fcc7-37fd-4761-b6b1-7b502aeb5bf2", "Toyota", 10063m, 1997 },
                    { new Guid("3cd9d349-82f6-470c-a23e-6e357b4f32ba"), "2dfba345-9458-457a-b3fd-a4809cbe2830", "Toyota", 10976m, 1994 },
                    { new Guid("3e621da4-0b81-4072-ba5b-734dd95686f1"), "4e8c292a-d396-43cf-9948-1ae9878f8213", "Lamborghini", 8512m, 1966 },
                    { new Guid("3f1acc07-bbb5-43f8-920d-5868e3efc66c"), "b99819ba-4b98-48cf-ae39-6ba71301d925", "Lamborghini", 24843m, 1990 },
                    { new Guid("3f424a35-609f-4cd4-8760-4a8e1945ff36"), "d16ce965-9b40-4914-b53a-8e7e72fed813", "Mercedes Benz", 17036m, 1958 },
                    { new Guid("41b148c0-466c-4c17-b7cd-655611919d1b"), "582b711b-bdbd-42d0-8ce3-388e9e2ec0e0", "Ferrari", 8173m, 1988 },
                    { new Guid("44efc62c-68de-4a1f-b7b3-3dbea7df5b7a"), "1f14afb9-df0d-49b5-a29e-8f3f7a1acd3f", "Toyota", 8346m, 2003 },
                    { new Guid("45999a0e-cb81-4505-b71a-7831b2510ce5"), "8a8c58cd-2e45-4abd-bd00-5625cd5924c1", "Fiat", 6195m, 1985 },
                    { new Guid("4638d67d-a597-420c-89d1-e02b899967dd"), "e1011981-7e52-4528-881b-c26c908e04bc", "Smart", 3405m, 1953 },
                    { new Guid("463e0592-f580-4739-91e5-b650cd227bd2"), "6af27c93-549c-4920-b0c8-79c47f296162", "Tesla", 22236m, 1986 },
                    { new Guid("471218b2-6461-4042-83e6-2944d24f325b"), "ffca9482-b124-4596-8c5d-fda08168bb25", "Rolls Royce", 5282m, 1957 },
                    { new Guid("4866ef2f-e1a5-436c-b619-0a436c00051a"), "e7156a99-df18-40a8-ae29-27ee6d983ec6", "Bugatti", 6206m, 1997 },
                    { new Guid("4a460912-aba7-4066-8ec6-054f3255778c"), "f5e4d92f-02fe-4846-b75d-1155e6404546", "Mercedes Benz", 2792m, 2002 },
                    { new Guid("4c00e00d-b5c3-465e-b5c0-2a6839ead52b"), "af57ade6-9dfd-4595-808a-4e691b2c4bd5", "Toyota", 22440m, 1966 },
                    { new Guid("4cc60a88-e811-4721-9029-32ee6bd530f6"), "e0f818b1-4b42-4981-b0e2-6c97f67ea788", "Audi", 18563m, 2006 },
                    { new Guid("4f3ef718-6373-4643-99e1-2b14a26b5d75"), "112d2965-4974-454d-9c8e-bd56a19cd690", "Audi", 2097m, 2003 },
                    { new Guid("50e125eb-89d3-429a-865e-855c2e87f864"), "ae36a777-d70c-4a66-98b9-34a7bbfdae8b", "Bugatti", 17717m, 1982 },
                    { new Guid("518b9d16-b3e7-4331-ad2a-29aeaa85dbb5"), "51420df8-f384-4630-b5f4-ce52ec2a48aa", "Dodge", 21142m, 1976 },
                    { new Guid("533aa325-2407-4651-8b9d-f23727379aea"), "413f7b2b-0641-49cd-9c7c-97233bb183ce", "Rolls Royce", 1417m, 2002 },
                    { new Guid("545ab8bc-76f0-41f9-a2bc-cf87a4e63b62"), "46dbe099-768d-44b7-a3f1-176b5394fc5c", "Volkswagen", 11486m, 1999 },
                    { new Guid("56909362-e636-4704-bfbc-3d09623217d1"), "55e4add2-d6a2-4e2a-b15f-3de579f52dd1", "Chrysler", 12584m, 2011 },
                    { new Guid("58ef627b-d19e-415f-8919-ef5b43a7e034"), "21975019-f89e-4741-b380-ee2ecf1839a0", "Ford", 17362m, 1973 },
                    { new Guid("59c3aaa9-91f2-4684-85f3-d3efb52c73f2"), "5abe41eb-f3b7-4ba4-a6ff-2c56b1d0be8d", "Ferrari", 14789m, 1964 },
                    { new Guid("5b78dcfd-710d-42d2-bbce-b9afb705c442"), "156a428d-6261-483a-a613-889200f3d969", "Maserati", 5494m, 1987 },
                    { new Guid("5bbe6fba-7299-4d78-8d53-c9cdaa800522"), "b2068aa0-f9a8-41dc-bddd-095f43c44cc3", "Smart", 12822m, 2011 },
                    { new Guid("5cafcd92-65cd-4378-972c-aaa21638e50d"), "45eab6bf-af5d-47e0-a951-67601855d1f7", "Rolls Royce", 18598m, 1997 },
                    { new Guid("5d416e3a-e79e-44e4-9c18-4739226a07c3"), "2e20d111-7c6b-4fb4-b5b8-eb53dd8f05ea", "Jeep", 4207m, 2018 },
                    { new Guid("5d51f6c1-3270-4c6d-b63e-70a7694415b1"), "7b22499f-d12a-4d17-96f3-acc3eb8de1a9", "Chevrolet", 4927m, 1974 },
                    { new Guid("600a9828-d202-4368-b38a-4819849bc42a"), "0ab05b1f-a655-4e17-8d4c-e8b3a53ae2b3", "Toyota", 11841m, 1999 },
                    { new Guid("609aa663-f3fc-4108-abd2-ccacd4299b83"), "c00af2df-38fd-4498-a95c-717e6537fbd3", "Hyundai", 13933m, 2012 },
                    { new Guid("677b0dd8-4c71-4c5f-8c85-22113ead3286"), "e804ef1c-82a7-4140-b7ca-4526fa1ed623", "Hyundai", 13614m, 1956 },
                    { new Guid("69c0f0a0-f7b6-41f9-9fbe-9ab37661db54"), "87204995-c006-4d7f-8ca4-08c814495793", "Cadillac", 2089m, 1990 },
                    { new Guid("6b4c2eb8-669b-4c17-aea6-5df72a236a85"), "b88e3b2e-c1e3-41b4-ab97-4b40085d8310", "Kia", 23862m, 2015 },
                    { new Guid("6bb7c97d-2841-484a-910c-32969ef23636"), "113a70e5-0763-4da3-99cb-fd5a12fe9fd9", "Kia", 10127m, 2016 },
                    { new Guid("6bfeafa5-f8bc-4a49-aba8-f56243d2cd47"), "5b9342d6-cdd3-4fe9-9f6c-7774294772e3", "Mini", 18731m, 1953 },
                    { new Guid("6d389a21-b596-49b5-b243-8f851550bb47"), "9bdbc496-fbc9-49d8-88b1-a61aa9b5db57", "Smart", 4180m, 1958 },
                    { new Guid("6ef952c2-c3c0-4180-8167-aedb4d026e9f"), "91cabb76-2295-43c2-9563-a4296ae9c8d0", "Porsche", 15497m, 2020 },
                    { new Guid("6f1ea977-104a-43b7-bc10-ec39aea1cd20"), "027979e4-288d-420c-9574-e97eef18077e", "Nissan", 10396m, 1987 },
                    { new Guid("6fa4c435-78eb-4f3e-a075-06cab98c3198"), "b509bd10-f5da-4799-b87e-9c73bfba0a42", "Volkswagen", 6573m, 1992 },
                    { new Guid("71d78121-9f22-405a-b738-c3116eb93a74"), "44f56992-9c62-4131-a1c5-5a00356dae43", "Aston Martin", 7933m, 1962 },
                    { new Guid("7296b084-3e87-4853-a473-8f79804ba7a3"), "5bbfa9ea-e743-4da9-8587-58e540288b2d", "Toyota", 18197m, 2019 },
                    { new Guid("73f6f9e5-7afb-4699-a3e1-45b5fa45922e"), "2961d49d-e28e-4486-b2e0-d8d3833ffe2f", "Kia", 24086m, 1998 },
                    { new Guid("7475bc0c-07c8-4a18-9c45-b90ecf337a71"), "424ab7aa-e5b8-4682-9217-29e6451ab11b", "Fiat", 13659m, 1965 },
                    { new Guid("7545d9f1-bffa-4848-b68e-df260a4d1140"), "75980872-acaa-4aab-a6a7-d7d9692a685e", "Mini", 1931m, 1972 },
                    { new Guid("760d1151-e738-481b-b987-f3f5f6b5f49a"), "6e4666e4-4faf-4a09-bd79-9192dabe9da3", "Ford", 8979m, 1959 },
                    { new Guid("76e79ee0-0c8e-4e1b-b5c2-366500aec831"), "5502c292-74bd-4003-84ad-4e157f23767f", "Bentley", 17765m, 2009 },
                    { new Guid("770bc526-c109-45df-bba1-521146a9936c"), "1f21cdf0-6b8a-4c4e-8e0d-38a4a450895a", "Jaguar", 8795m, 1964 },
                    { new Guid("774c821b-44ea-4ebc-927c-7093f2fd32aa"), "582218dd-8782-4aa8-b18a-c959914dea1b", "Maserati", 1525m, 1962 },
                    { new Guid("77a04ed0-7dfb-4751-a4fa-a848d508d532"), "7f131147-2dc5-45dc-b8d7-dbae18831450", "Aston Martin", 3873m, 1972 },
                    { new Guid("784cd549-04d9-4ac4-b649-62b2085ab72e"), "f79a14d4-8739-4ad5-afc0-7e84d130cd16", "Dodge", 23520m, 1992 },
                    { new Guid("788d9b91-8d39-4bc9-ba79-970e1a73af3e"), "13b9b523-297a-4a9b-9227-1d6c24672c6c", "Toyota", 2971m, 1973 },
                    { new Guid("7a29fb2c-a34d-4a50-8d90-a5bf8145b7ab"), "93fcf149-d4ff-4a21-b30f-1eb852b8238c", "Audi", 3044m, 2021 },
                    { new Guid("7d98a384-d64c-47c6-a551-d661f722b591"), "3048fdb2-1cac-4f49-8597-632ee264ce9d", "Polestar", 16034m, 1977 },
                    { new Guid("851a67aa-b7af-40ed-8663-21a32e4fe56a"), "99b2841d-7075-484c-a631-fc8275abc63a", "Land Rover", 18093m, 1958 },
                    { new Guid("8755e6d9-50db-440c-968c-609a34c72d86"), "78bf9d06-b0c7-46fc-8599-82a18d8dc0c3", "Aston Martin", 24636m, 1954 },
                    { new Guid("88db8d45-9ea0-4bf7-bfe2-785d1cf6838e"), "2db5a88d-a748-425c-bfae-615ad801bb47", "Bentley", 22065m, 1956 },
                    { new Guid("88f923cc-9b3c-4db1-ba7c-680e38ce2744"), "835f4ecf-9719-4e1a-9424-6ffc4b26e62d", "Cadillac", 4199m, 1986 },
                    { new Guid("921b441c-5d2e-44c8-8207-543190ed65ac"), "cf806be0-adc5-44c4-b1b2-be161d6242b6", "Jeep", 22519m, 1954 },
                    { new Guid("926f15dd-fb11-4396-b1cf-01528670edd2"), "9cf414d6-4455-4c11-8ee3-1afea5e60548", "Hyundai", 20934m, 2022 },
                    { new Guid("9307bae3-ce44-4853-823a-80ff412a4fc4"), "af2804d3-ce9c-4da8-ac7f-e1baf7e2b0a6", "Jaguar", 7774m, 1957 },
                    { new Guid("9566e25f-97e0-4053-b56b-122d62a61349"), "a38e5944-2890-48e0-9bdf-0e11309f84b2", "Polestar", 6343m, 1978 },
                    { new Guid("95e85db5-52b0-4238-b16c-eb818b23b532"), "7cae1850-06e7-4516-accb-80e6b69e72fc", "Jaguar", 7239m, 1955 },
                    { new Guid("95f5e766-b72d-4e95-8139-607b1c36398e"), "7b65a4ce-fd60-4421-9d4b-75bca9cade9c", "Audi", 1286m, 2016 },
                    { new Guid("962f40e7-2c85-45ff-ad3f-867db2c41e3a"), "75ae4211-fa45-4a9c-ab15-57276f854b42", "Toyota", 2653m, 1993 },
                    { new Guid("96874aaa-1cf2-4895-b75f-e1f7be3977dc"), "219e4581-5073-40bc-9cb8-ba053f10e036", "Hyundai", 12598m, 2021 },
                    { new Guid("980ab233-d90a-4d17-af05-12ea73c21a4c"), "c3bc9c30-079c-42a0-ba74-9b836b30e6fa", "Chevrolet", 2987m, 1956 },
                    { new Guid("99101f6e-3ece-4007-8b2c-fd7553f1dc7d"), "d8778b85-3d01-430d-83ee-fbadc6dc79a8", "Hyundai", 8476m, 1960 },
                    { new Guid("9da0a344-7c6c-48ff-8620-f6b191f71822"), "ea40fd3e-4fe1-4ff0-98f5-e391dc8995b2", "Aston Martin", 12303m, 1981 },
                    { new Guid("9ded0bb6-9288-4b50-ba28-eb259ec50c3b"), "38bb0d86-13a4-4b4f-893a-3d256daccf12", "Land Rover", 13676m, 1988 },
                    { new Guid("a33efc7f-70c8-417c-bc2e-5cfa017783b2"), "a1e2ae17-de93-4e1e-bde4-cebbe982b7dd", "Volvo", 7576m, 2016 },
                    { new Guid("a4eba18b-35a8-4cd8-80b1-e90b00616ca3"), "9076f908-b697-4355-8eb3-02ce8f434379", "Dodge", 3231m, 1980 },
                    { new Guid("a502e588-8e5a-474b-8c46-afe7b07e6cfc"), "85b0b3db-3c4d-47da-aee5-d90a16140c81", "Chevrolet", 21574m, 1988 },
                    { new Guid("a61d89f0-8329-4641-8cc3-46e314b3902b"), "3935d7c8-bffe-4271-86d4-fa4848e2eadb", "Hyundai", 1502m, 2019 },
                    { new Guid("a6bb6784-1532-4dff-b012-f39f6b0dad6b"), "8159bfd8-dc43-4eaf-8bdd-00a36938f43e", "Dodge", 20945m, 1962 },
                    { new Guid("aa75ac11-a480-4509-847a-e55e37ee372f"), "f9867c91-6b5f-40a7-ad82-c56916f53104", "Tesla", 13757m, 1998 },
                    { new Guid("aa9c9cba-1cb4-41ef-bb47-4edc0d8f74a5"), "5f1c4fe4-cfeb-410c-814c-cb7c288f47fa", "Mazda", 18648m, 1965 },
                    { new Guid("ab19489f-8d31-402f-a7e2-9b0fff89ea4f"), "200ee6d6-20ea-4588-a54d-1897dfc7096c", "Hyundai", 14336m, 1958 },
                    { new Guid("abd981f7-a216-41d5-be39-177dfc22691e"), "0298c01a-03d0-4ee8-a332-aadc1861ddab", "Porsche", 12242m, 2016 },
                    { new Guid("ace73092-fc93-4328-b906-78b8190d87f3"), "db77f24a-e7a3-4812-82c9-8f2db7959b5e", "Bentley", 11510m, 1971 },
                    { new Guid("ad596250-ccd9-4469-a07a-d04fa6279791"), "415de7f1-a58e-48ae-90e8-a8d067f85f2e", "Mini", 4119m, 1989 },
                    { new Guid("b0006fde-0963-4c28-bfa0-8ae63bcc9ec3"), "57374af5-0873-4d92-bd46-91430daa101c", "Nissan", 15296m, 1970 },
                    { new Guid("b01ffc0e-c28a-461c-b23d-24b2d5f396ea"), "66d3b5cf-cf99-41b4-9da5-6160d31e0fc0", "Volvo", 2805m, 1984 },
                    { new Guid("b5b23b9f-8666-446d-9b87-b3432fbfff50"), "4abc1ec7-0bfd-4581-910e-01c42f5ddf04", "Chevrolet", 3649m, 1959 },
                    { new Guid("b5c574c6-4f01-4b80-8b67-5ec5e1794458"), "6bdef035-42f2-40aa-95e5-3f0aa1c3f2f2", "Honda", 17113m, 1975 },
                    { new Guid("b6b57aa9-1145-46e5-9720-5ae75ddd44a2"), "d32fa881-1911-484f-8c13-76e678b71510", "Volkswagen", 15503m, 1965 },
                    { new Guid("b7075f0e-2fcb-491b-8991-d621ef31587c"), "e778ec31-a7fa-45cf-9faf-af52d3f9fdc8", "Ferrari", 21742m, 2005 },
                    { new Guid("b716099b-fd43-422c-8471-8d43b5108715"), "6398697e-962c-4218-a6df-b5bdcc8fbaa2", "Nissan", 13489m, 1950 },
                    { new Guid("b75bb224-659d-4646-a111-2eaf6c63c64a"), "90f3847a-f08c-412a-9d28-723583a6ac14", "Mini", 17004m, 2019 },
                    { new Guid("b827281b-2a1d-4b2e-b280-6450a413db5d"), "fd78b712-8baf-4a1d-a5c1-df441727a189", "Aston Martin", 6230m, 1993 },
                    { new Guid("b9e558b0-ab82-42b7-9545-699ccba32a2b"), "f4f1918b-9058-4a23-8496-16244450ae43", "Fiat", 20145m, 1962 },
                    { new Guid("bc16dd94-c681-4593-9afb-398bca35c14f"), "22b8342b-0a64-4220-abb7-84d0e2772c56", "Jeep", 24939m, 2000 },
                    { new Guid("c02e9689-d460-48a9-b41f-a2556545cfcb"), "b0e8ce94-af3c-40ae-afa5-16be2aafb434", "Ford", 19209m, 1973 },
                    { new Guid("c1fe2c94-0abf-49c6-a676-78c64720ed1e"), "0a83aa7c-0eb8-471c-be37-0cb0cf212a1b", "Bentley", 15086m, 1999 },
                    { new Guid("c2a79f02-16a1-49b1-8242-cf1757e19def"), "3815e008-5f84-4b21-8363-ff2c48ae2e7a", "BMW", 23878m, 2010 },
                    { new Guid("c4883485-fcfa-4255-8984-bc9510dc7133"), "aa423734-07e5-47d5-8bf0-844f59e8dcbc", "Kia", 18003m, 1964 },
                    { new Guid("c4d1ed7c-cfd7-4a29-ac39-d04f27162506"), "1265d9f3-9ee3-40a4-add6-b441447aa2d9", "Jaguar", 7403m, 2006 },
                    { new Guid("c4fd59f6-9907-4e90-a575-f54c37019b19"), "95232301-1d33-4340-8322-eeb495c83fb6", "Jaguar", 14545m, 1968 },
                    { new Guid("c585bd23-2fca-4534-969d-6a693c7aeaf0"), "b4cb929b-31e3-4a35-b860-6dae1771176e", "Cadillac", 22566m, 1985 },
                    { new Guid("c6281cbc-e03f-4473-b450-00c06172c1da"), "6b6ff99e-7e6f-4901-8480-2a88db8ce0e1", "Honda", 21432m, 2019 },
                    { new Guid("c65199a8-b5b2-4452-9f04-be0022d0bab0"), "1099cb5a-533f-4c42-842a-526eff1305b8", "Bugatti", 5424m, 1969 },
                    { new Guid("c915ed0e-0358-4a2f-a84e-b21400ffd71b"), "970f36a9-a6f0-480f-9847-bae7674ce0ef", "Tesla", 4694m, 1976 },
                    { new Guid("c98d335c-3665-443a-bda1-17cf788eb76e"), "021b6053-151a-4597-adb5-cc90b4f53002", "Polestar", 17946m, 1978 },
                    { new Guid("ca0d6a40-40da-40e1-b1ac-7597f76ccbe8"), "4d5cbee4-0474-4d38-9764-24fc6974e902", "Mini", 1232m, 2003 },
                    { new Guid("cc290d7f-a4f7-4c72-aeff-daac5cf0de97"), "0ba65b1d-cadd-43cb-9647-2b6c587e31e0", "Porsche", 8427m, 1975 },
                    { new Guid("cc528f66-c65c-4fcb-8bda-44ca168dd0dd"), "521b2db8-1524-4c79-ba38-569376c54532", "Tesla", 1994m, 1994 },
                    { new Guid("cc75d6ad-c39d-4f20-a8e7-5f1dd94971ef"), "847968c6-3c7a-44ea-990e-578ac2e13378", "Rolls Royce", 24677m, 1978 },
                    { new Guid("cd6f85dd-888e-440f-bbb9-293c4642ad42"), "19d9cd64-7935-4523-8ab8-11e946841fd8", "Lamborghini", 20283m, 1963 },
                    { new Guid("cd84c63b-5475-4545-8fba-8777a06e67c3"), "d35b6e6a-bd03-400b-94a2-fade4ccec7a3", "Chrysler", 13835m, 1958 },
                    { new Guid("ce9c4eac-d801-4b4f-b6f6-2400da2c960c"), "fc92ceec-fc65-435c-b95a-ae6df56ca8ea", "Aston Martin", 22674m, 1962 },
                    { new Guid("cf77c942-7e61-449a-b975-172fe30aae6d"), "bcd3b6ef-1deb-4070-a7d3-f21551da237d", "Land Rover", 17892m, 1977 },
                    { new Guid("d021089c-6b4a-44d4-b34a-9bf90c0119a1"), "6374a3ab-8230-4a1f-b10f-599a444b8604", "Aston Martin", 20025m, 1977 },
                    { new Guid("d0b643ec-8e24-4772-81e9-6c8242d2c8d1"), "36f9dd61-e8f2-4039-b0e7-8cd17a0ba242", "Jaguar", 16171m, 1963 },
                    { new Guid("d1013fba-55ab-4fa6-b71c-5a42db608a59"), "9354471e-713e-4e66-9fff-dc315d2db559", "Tesla", 4951m, 1975 },
                    { new Guid("d14c5914-4e82-421b-a8f0-a174bd14c20e"), "00e23394-86fd-4885-94ca-9429f9ae66ec", "Aston Martin", 13803m, 1957 },
                    { new Guid("d2a53d97-ee10-47f6-90bb-0010e93f5a7b"), "2fc6f4bb-acf8-4299-a477-28f8fcf2d07b", "Land Rover", 7192m, 1972 },
                    { new Guid("d437f289-d1b3-4e25-b258-0f55b098f7cb"), "f81ee63a-6c1e-45ed-bff7-64ac07c23441", "Volvo", 2865m, 1980 },
                    { new Guid("d49110db-81ca-4b36-b419-6020d61ac5fa"), "48616902-b1a6-4963-96d7-a8cbde51b46e", "Rolls Royce", 11023m, 1983 },
                    { new Guid("d80c6435-7eb6-4aa7-bbf5-34f90faf0d0f"), "0a7d5dcb-4440-4e21-832a-1f1cbb90c5f7", "Ford", 21355m, 1952 },
                    { new Guid("dad57c2c-445a-4ed7-baa4-9da265f631dd"), "20df1e24-192a-4ca2-850f-e303a934d6a6", "Mercedes Benz", 10636m, 2020 },
                    { new Guid("dafb1274-96c4-4a35-9948-cf06537ec619"), "e104f213-927c-409b-8a4d-ce0a6e282246", "Aston Martin", 21274m, 1976 },
                    { new Guid("dbd3df4c-1b71-47ee-aee2-96e6d57f7602"), "1a29c2b4-4200-4de4-923f-1f9c70afe9b4", "Chevrolet", 11689m, 2017 },
                    { new Guid("dbd99461-8db0-448a-8062-9d80fa6d1d68"), "49b01097-ae0a-488b-8c86-c8f99f1572c8", "Fiat", 2398m, 2003 },
                    { new Guid("dd433925-7b04-4a0b-884f-749b771c3a3e"), "6993e280-38cb-4fe5-b250-d72f0f0f828c", "Cadillac", 16398m, 2017 },
                    { new Guid("deafc5e2-8d38-455c-9b02-ac325771413d"), "797025a2-24c7-4673-903b-3472a800aab6", "Audi", 10526m, 1995 },
                    { new Guid("dfc4f83d-0945-452a-8e31-a96fa4868802"), "9b10b1ac-1b0c-4d17-8e26-3eadce8bed13", "Ford", 2340m, 1986 },
                    { new Guid("e10423f9-3c42-4cfe-b58d-dc8a4b828a3f"), "22dfa04b-d4e9-4e90-9553-089cb67196bc", "Toyota", 16032m, 1999 },
                    { new Guid("e1140e74-2aa4-4580-bba0-59020dc60e5c"), "43191fcf-c325-4b82-bd65-0db85b122706", "Land Rover", 14435m, 1982 },
                    { new Guid("e1a1efb9-397d-414d-bc0b-d4020912d83d"), "a652a803-1ae9-4bfc-82eb-827f58ab51dc", "Tesla", 7314m, 2006 },
                    { new Guid("e1c9a9ab-91e6-4571-baf8-2581b6f53b60"), "c87189ca-ba7c-4f2c-add1-4152f871d2c5", "Mercedes Benz", 8913m, 2024 },
                    { new Guid("e1e2c722-e2f7-45c2-b3d0-914c364c2dc6"), "698f8517-b341-448f-98c2-e8b405f494c8", "Chevrolet", 14063m, 1997 },
                    { new Guid("e292d8e8-f712-47c8-8903-537092be5ca7"), "aba60d72-2ecd-4338-8f87-63ffec168284", "Porsche", 24385m, 2009 },
                    { new Guid("e3ff59f1-ef9a-4b40-93f8-965b00f34f1c"), "61e956b7-af15-402f-98d7-e3825581e981", "Volkswagen", 14195m, 2005 },
                    { new Guid("e51aedc3-8bd5-452d-a9cc-bed68c8432bc"), "a44c324a-0abc-4666-aef3-4097453531e0", "Nissan", 12550m, 1970 },
                    { new Guid("e6c868e6-24cb-44ab-a45a-2c05e4a6efc7"), "6d5e38c1-da25-41a5-9a87-65742021faf5", "Fiat", 20842m, 1956 },
                    { new Guid("e77e91a0-d7f0-4369-9a0e-9fcd6c263a1f"), "54118081-4baa-4a5a-9841-56e4cf8b0ace", "Honda", 23930m, 1992 },
                    { new Guid("ee01dca5-9b55-4505-a263-d3fdc19c7892"), "8bc776cd-2655-4b18-886c-90b256a8ba62", "Tesla", 7187m, 1997 },
                    { new Guid("eedd5bd7-63f2-41ca-8f62-6ad519acf1be"), "7c8e4d54-5ed4-46eb-b890-21bfd580e2a5", "Fiat", 13539m, 1973 },
                    { new Guid("eefcfd2f-1078-4684-8922-a55d71e35d62"), "995cb171-f183-4ca2-ba61-b3aa4267057c", "Ford", 18025m, 1954 },
                    { new Guid("ef58a7eb-1968-46b1-8875-71f4e2f9a269"), "b6fb555a-22a0-4a5a-9ef3-66d2e2b70a4d", "Cadillac", 7309m, 2012 },
                    { new Guid("f0332a9b-1378-4ea7-809a-6eeab707f1e6"), "86020906-4982-49ff-8276-2fd382829007", "Dodge", 13815m, 1983 },
                    { new Guid("f063a19a-c255-4ff8-8c92-7b1753be5387"), "2df79bfc-919b-477e-b940-e7a52787ef05", "Ford", 18258m, 1963 },
                    { new Guid("f1c34437-ec0f-4c2f-97dd-c531b009fc66"), "705be1f0-2bd5-4c9a-83a9-ba42f0c6fc15", "Rolls Royce", 6636m, 2016 },
                    { new Guid("f2c607fd-5fbd-45ea-8019-2381c17b22fd"), "0120c170-8f57-4740-943f-466a55746823", "Jeep", 23324m, 2015 },
                    { new Guid("f3db3695-90e7-4077-aeb9-9b8d5f617d02"), "174c1ac8-7e02-485f-ade8-2c44bcd289cb", "Jeep", 3473m, 1957 },
                    { new Guid("f400de0c-686f-4e8a-9c8c-a21e91e976da"), "3172cbd6-50e3-48b3-81e3-8e7f3935eaae", "Aston Martin", 5215m, 2003 },
                    { new Guid("f40f3593-47de-49c0-a37b-62cdd81ae77f"), "91134e43-087f-4613-912e-91dd2a963208", "Maserati", 18802m, 2010 },
                    { new Guid("f4480985-c83f-42a9-b0d4-70a7ceb8463f"), "bc948efa-f7d6-4b18-81ff-0adce5a2ed8d", "Aston Martin", 23285m, 1964 },
                    { new Guid("f5a8722d-e985-424a-9a7f-0a69228e58ba"), "3e4dcbf4-d9b5-4dbf-9909-c04bdb9672c4", "Jaguar", 1377m, 2005 },
                    { new Guid("f5aa55a4-fa22-4441-a4dc-7bacc48c2682"), "f5a84b8f-5307-45b9-a000-cdac56b3ba31", "Chevrolet", 14927m, 1978 },
                    { new Guid("f6eed512-1161-468c-b52d-19a552852ed8"), "5427c4c3-f12f-4641-b46e-c82a7f0619e6", "Bentley", 12392m, 1981 },
                    { new Guid("f8ae6ac4-b7e2-4b54-95ba-cd2630360bda"), "63cbec96-8795-4b45-b8bf-97b595362d20", "Kia", 19522m, 1991 },
                    { new Guid("fa3fb964-0f92-4a7e-9c65-1f51559434fa"), "e0d260e6-dade-4e64-b938-c965bbcd9b4e", "Polestar", 20117m, 2014 },
                    { new Guid("fccd3a2e-6418-4f9a-a45e-405348ecdcdf"), "3957eccd-a7a4-4e16-8c4a-22a0fff7564f", "Land Rover", 6549m, 2002 },
                    { new Guid("ffb9cce9-3e88-4f3a-9803-6ec73c24fd20"), "0fac0cad-6200-4bea-a0ae-acc36d993b0b", "BMW", 18594m, 1992 }
                });

            migrationBuilder.InsertData(
                table: "HatchBack",
                columns: new[] { "Id", "NumberOfDoors" },
                values: new object[,]
                {
                    { new Guid("05e166c7-583e-4d63-8f80-c65f66cbf775"), 1 },
                    { new Guid("0baaa524-da79-434d-ba2d-66b337926401"), 3 },
                    { new Guid("0bd54979-24bf-4075-bb13-2f1152cd9319"), 2 },
                    { new Guid("17536aeb-1bc1-4e7b-8b56-a357e8e49d43"), 1 },
                    { new Guid("1a16c35c-3277-4e83-b0ad-f776947d0a35"), 5 },
                    { new Guid("1ca7119b-7d8d-4cf2-ab57-77c062e33403"), 3 },
                    { new Guid("1e5debd4-b592-4380-b5d4-47c200a91e36"), 3 },
                    { new Guid("21eee76f-8e0d-42f8-9538-21e1ed90a81a"), 2 },
                    { new Guid("2a9a73f5-c374-4635-a887-a9b24eb2cd63"), 2 },
                    { new Guid("2e95018a-3f30-4829-9074-8f184cc18c13"), 5 },
                    { new Guid("2f7036db-bc1c-4dde-b3d4-712643493294"), 3 },
                    { new Guid("30ac5435-a5c4-4b97-afcd-bf0407cf60a8"), 5 },
                    { new Guid("32135ae4-ee23-4ec6-a8df-457ad50303be"), 1 },
                    { new Guid("3a133a3a-e05b-47d0-ab37-9bf703dae72d"), 2 },
                    { new Guid("3e621da4-0b81-4072-ba5b-734dd95686f1"), 4 },
                    { new Guid("3f424a35-609f-4cd4-8760-4a8e1945ff36"), 3 },
                    { new Guid("463e0592-f580-4739-91e5-b650cd227bd2"), 1 },
                    { new Guid("4a460912-aba7-4066-8ec6-054f3255778c"), 3 },
                    { new Guid("56909362-e636-4704-bfbc-3d09623217d1"), 5 },
                    { new Guid("5d416e3a-e79e-44e4-9c18-4739226a07c3"), 5 },
                    { new Guid("600a9828-d202-4368-b38a-4819849bc42a"), 5 },
                    { new Guid("609aa663-f3fc-4108-abd2-ccacd4299b83"), 5 },
                    { new Guid("69c0f0a0-f7b6-41f9-9fbe-9ab37661db54"), 2 },
                    { new Guid("6fa4c435-78eb-4f3e-a075-06cab98c3198"), 2 },
                    { new Guid("760d1151-e738-481b-b987-f3f5f6b5f49a"), 3 },
                    { new Guid("7d98a384-d64c-47c6-a551-d661f722b591"), 5 },
                    { new Guid("88f923cc-9b3c-4db1-ba7c-680e38ce2744"), 5 },
                    { new Guid("9566e25f-97e0-4053-b56b-122d62a61349"), 5 },
                    { new Guid("980ab233-d90a-4d17-af05-12ea73c21a4c"), 2 },
                    { new Guid("a33efc7f-70c8-417c-bc2e-5cfa017783b2"), 5 },
                    { new Guid("ace73092-fc93-4328-b906-78b8190d87f3"), 5 },
                    { new Guid("b0006fde-0963-4c28-bfa0-8ae63bcc9ec3"), 4 },
                    { new Guid("b7075f0e-2fcb-491b-8991-d621ef31587c"), 1 },
                    { new Guid("b75bb224-659d-4646-a111-2eaf6c63c64a"), 4 },
                    { new Guid("b827281b-2a1d-4b2e-b280-6450a413db5d"), 2 },
                    { new Guid("bc16dd94-c681-4593-9afb-398bca35c14f"), 1 },
                    { new Guid("c2a79f02-16a1-49b1-8242-cf1757e19def"), 3 },
                    { new Guid("cc290d7f-a4f7-4c72-aeff-daac5cf0de97"), 4 },
                    { new Guid("cd84c63b-5475-4545-8fba-8777a06e67c3"), 4 },
                    { new Guid("d80c6435-7eb6-4aa7-bbf5-34f90faf0d0f"), 1 },
                    { new Guid("dd433925-7b04-4a0b-884f-749b771c3a3e"), 1 },
                    { new Guid("e3ff59f1-ef9a-4b40-93f8-965b00f34f1c"), 3 },
                    { new Guid("e51aedc3-8bd5-452d-a9cc-bed68c8432bc"), 1 },
                    { new Guid("e6c868e6-24cb-44ab-a45a-2c05e4a6efc7"), 2 },
                    { new Guid("e77e91a0-d7f0-4369-9a0e-9fcd6c263a1f"), 2 },
                    { new Guid("ef58a7eb-1968-46b1-8875-71f4e2f9a269"), 3 },
                    { new Guid("f5a8722d-e985-424a-9a7f-0a69228e58ba"), 4 },
                    { new Guid("f5aa55a4-fa22-4441-a4dc-7bacc48c2682"), 4 },
                    { new Guid("f8ae6ac4-b7e2-4b54-95ba-cd2630360bda"), 1 },
                    { new Guid("ffb9cce9-3e88-4f3a-9803-6ec73c24fd20"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sedan",
                columns: new[] { "Id", "NumberOfDoors" },
                values: new object[,]
                {
                    { new Guid("0313a818-eac2-497b-b7bd-83c9881c1fa7"), 5 },
                    { new Guid("044f1e5a-037e-4f68-bcdf-069daae12594"), 1 },
                    { new Guid("045e152a-1ee3-459b-a2eb-f2513b89af2d"), 1 },
                    { new Guid("0738449b-28e6-4d74-bc27-2cb2744c5a87"), 4 },
                    { new Guid("0a2b3679-8764-46b2-85ac-31f2a7efa862"), 3 },
                    { new Guid("0edd857a-2f94-4e8f-8f48-bc2f851b5f51"), 2 },
                    { new Guid("1365d434-3509-4072-80b0-0a31bd80e6c2"), 4 },
                    { new Guid("137a4a7a-c6b5-47c0-9e9b-a4e289df5c62"), 2 },
                    { new Guid("19046a1f-99ac-4225-928b-7680a96c5eb9"), 5 },
                    { new Guid("28c76101-741b-4c0b-9a9e-4f50bb4538aa"), 1 },
                    { new Guid("2d9cefe6-3cf2-477d-bba5-70c1f7d903d6"), 4 },
                    { new Guid("339f7753-ff75-4fb5-a095-7514ef872ebd"), 5 },
                    { new Guid("371c02d8-8f34-43ee-b0c5-3052ae70b583"), 2 },
                    { new Guid("45999a0e-cb81-4505-b71a-7831b2510ce5"), 2 },
                    { new Guid("4638d67d-a597-420c-89d1-e02b899967dd"), 2 },
                    { new Guid("4cc60a88-e811-4721-9029-32ee6bd530f6"), 5 },
                    { new Guid("5bbe6fba-7299-4d78-8d53-c9cdaa800522"), 2 },
                    { new Guid("677b0dd8-4c71-4c5f-8c85-22113ead3286"), 5 },
                    { new Guid("6bfeafa5-f8bc-4a49-aba8-f56243d2cd47"), 1 },
                    { new Guid("6d389a21-b596-49b5-b243-8f851550bb47"), 3 },
                    { new Guid("6ef952c2-c3c0-4180-8167-aedb4d026e9f"), 4 },
                    { new Guid("7296b084-3e87-4853-a473-8f79804ba7a3"), 2 },
                    { new Guid("7475bc0c-07c8-4a18-9c45-b90ecf337a71"), 4 },
                    { new Guid("76e79ee0-0c8e-4e1b-b5c2-366500aec831"), 5 },
                    { new Guid("774c821b-44ea-4ebc-927c-7093f2fd32aa"), 5 },
                    { new Guid("788d9b91-8d39-4bc9-ba79-970e1a73af3e"), 4 },
                    { new Guid("7a29fb2c-a34d-4a50-8d90-a5bf8145b7ab"), 1 },
                    { new Guid("96874aaa-1cf2-4895-b75f-e1f7be3977dc"), 3 },
                    { new Guid("9da0a344-7c6c-48ff-8620-f6b191f71822"), 1 },
                    { new Guid("a4eba18b-35a8-4cd8-80b1-e90b00616ca3"), 5 },
                    { new Guid("ab19489f-8d31-402f-a7e2-9b0fff89ea4f"), 4 },
                    { new Guid("abd981f7-a216-41d5-be39-177dfc22691e"), 1 },
                    { new Guid("ad596250-ccd9-4469-a07a-d04fa6279791"), 2 },
                    { new Guid("b5b23b9f-8666-446d-9b87-b3432fbfff50"), 4 },
                    { new Guid("b9e558b0-ab82-42b7-9545-699ccba32a2b"), 4 },
                    { new Guid("c1fe2c94-0abf-49c6-a676-78c64720ed1e"), 3 },
                    { new Guid("c4883485-fcfa-4255-8984-bc9510dc7133"), 5 },
                    { new Guid("c4fd59f6-9907-4e90-a575-f54c37019b19"), 2 },
                    { new Guid("c6281cbc-e03f-4473-b450-00c06172c1da"), 4 },
                    { new Guid("ca0d6a40-40da-40e1-b1ac-7597f76ccbe8"), 4 },
                    { new Guid("cd6f85dd-888e-440f-bbb9-293c4642ad42"), 3 },
                    { new Guid("d021089c-6b4a-44d4-b34a-9bf90c0119a1"), 4 },
                    { new Guid("d2a53d97-ee10-47f6-90bb-0010e93f5a7b"), 3 },
                    { new Guid("d49110db-81ca-4b36-b419-6020d61ac5fa"), 2 },
                    { new Guid("dafb1274-96c4-4a35-9948-cf06537ec619"), 3 },
                    { new Guid("e1c9a9ab-91e6-4571-baf8-2581b6f53b60"), 3 },
                    { new Guid("ee01dca5-9b55-4505-a263-d3fdc19c7892"), 5 },
                    { new Guid("eedd5bd7-63f2-41ca-8f62-6ad519acf1be"), 5 },
                    { new Guid("f400de0c-686f-4e8a-9c8c-a21e91e976da"), 5 },
                    { new Guid("f4480985-c83f-42a9-b0d4-70a7ceb8463f"), 4 }
                });

            migrationBuilder.InsertData(
                table: "Suv",
                columns: new[] { "Id", "NumberOfSeats" },
                values: new object[,]
                {
                    { new Guid("03948eae-630a-4ec6-9d9c-353314ce2f66"), 7 },
                    { new Guid("0eb0eb30-1b45-484b-a907-2725d8c57d0a"), 7 },
                    { new Guid("0f8c2fa3-e658-467e-aef9-65258c184378"), 5 },
                    { new Guid("197c99d5-e25b-458b-af8d-761e81d7a830"), 5 },
                    { new Guid("1a090a94-f09c-4a0e-a479-fc36c592ad7a"), 9 },
                    { new Guid("1d66f16a-df11-4069-a591-4bca9211c702"), 8 },
                    { new Guid("1f10e3a3-0036-416a-a700-85560126f98d"), 8 },
                    { new Guid("288d541a-f698-467a-9039-89066f4135ff"), 5 },
                    { new Guid("2b0198b6-121b-4c94-903c-2eefc0d733a1"), 8 },
                    { new Guid("2ee6efbe-4b43-495c-9276-0240c9a60acc"), 7 },
                    { new Guid("3cd9d349-82f6-470c-a23e-6e357b4f32ba"), 9 },
                    { new Guid("3f1acc07-bbb5-43f8-920d-5868e3efc66c"), 5 },
                    { new Guid("41b148c0-466c-4c17-b7cd-655611919d1b"), 7 },
                    { new Guid("4c00e00d-b5c3-465e-b5c0-2a6839ead52b"), 5 },
                    { new Guid("4f3ef718-6373-4643-99e1-2b14a26b5d75"), 8 },
                    { new Guid("518b9d16-b3e7-4331-ad2a-29aeaa85dbb5"), 8 },
                    { new Guid("533aa325-2407-4651-8b9d-f23727379aea"), 9 },
                    { new Guid("58ef627b-d19e-415f-8919-ef5b43a7e034"), 5 },
                    { new Guid("5cafcd92-65cd-4378-972c-aaa21638e50d"), 6 },
                    { new Guid("71d78121-9f22-405a-b738-c3116eb93a74"), 5 },
                    { new Guid("7545d9f1-bffa-4848-b68e-df260a4d1140"), 9 },
                    { new Guid("770bc526-c109-45df-bba1-521146a9936c"), 8 },
                    { new Guid("77a04ed0-7dfb-4751-a4fa-a848d508d532"), 6 },
                    { new Guid("851a67aa-b7af-40ed-8663-21a32e4fe56a"), 8 },
                    { new Guid("88db8d45-9ea0-4bf7-bfe2-785d1cf6838e"), 9 },
                    { new Guid("921b441c-5d2e-44c8-8207-543190ed65ac"), 7 },
                    { new Guid("926f15dd-fb11-4396-b1cf-01528670edd2"), 9 },
                    { new Guid("95e85db5-52b0-4238-b16c-eb818b23b532"), 7 },
                    { new Guid("99101f6e-3ece-4007-8b2c-fd7553f1dc7d"), 9 },
                    { new Guid("9ded0bb6-9288-4b50-ba28-eb259ec50c3b"), 9 },
                    { new Guid("a61d89f0-8329-4641-8cc3-46e314b3902b"), 6 },
                    { new Guid("a6bb6784-1532-4dff-b012-f39f6b0dad6b"), 8 },
                    { new Guid("aa75ac11-a480-4509-847a-e55e37ee372f"), 9 },
                    { new Guid("b01ffc0e-c28a-461c-b23d-24b2d5f396ea"), 8 },
                    { new Guid("b716099b-fd43-422c-8471-8d43b5108715"), 8 },
                    { new Guid("c02e9689-d460-48a9-b41f-a2556545cfcb"), 8 },
                    { new Guid("c585bd23-2fca-4534-969d-6a693c7aeaf0"), 8 },
                    { new Guid("cc75d6ad-c39d-4f20-a8e7-5f1dd94971ef"), 5 },
                    { new Guid("ce9c4eac-d801-4b4f-b6f6-2400da2c960c"), 8 },
                    { new Guid("d0b643ec-8e24-4772-81e9-6c8242d2c8d1"), 9 },
                    { new Guid("d1013fba-55ab-4fa6-b71c-5a42db608a59"), 7 },
                    { new Guid("d437f289-d1b3-4e25-b258-0f55b098f7cb"), 7 },
                    { new Guid("dad57c2c-445a-4ed7-baa4-9da265f631dd"), 5 },
                    { new Guid("deafc5e2-8d38-455c-9b02-ac325771413d"), 8 },
                    { new Guid("e10423f9-3c42-4cfe-b58d-dc8a4b828a3f"), 6 },
                    { new Guid("e1140e74-2aa4-4580-bba0-59020dc60e5c"), 9 },
                    { new Guid("e1a1efb9-397d-414d-bc0b-d4020912d83d"), 6 },
                    { new Guid("e292d8e8-f712-47c8-8903-537092be5ca7"), 5 },
                    { new Guid("f6eed512-1161-468c-b52d-19a552852ed8"), 8 },
                    { new Guid("fa3fb964-0f92-4a7e-9c65-1f51559434fa"), 7 }
                });

            migrationBuilder.InsertData(
                table: "Truck",
                columns: new[] { "Id", "LoadCapacity" },
                values: new object[,]
                {
                    { new Guid("009daeb8-a2ef-4a5f-a65e-15223dcc9f73"), 28208 },
                    { new Guid("0de917f8-7db4-40aa-b7f4-3e56ff3baee9"), 24149 },
                    { new Guid("19e197ee-bfdd-4054-9cb0-611f05810613"), 10695 },
                    { new Guid("1f640420-e15c-456a-b24a-8972a38f78db"), 19201 },
                    { new Guid("27e7b2fb-5c76-4ee5-a643-971be2127bf7"), 23061 },
                    { new Guid("28edf843-143d-49b6-8d8e-1795afd7269f"), 28896 },
                    { new Guid("2b22aa11-ebf0-464c-be6b-0c7cfb0c8364"), 30691 },
                    { new Guid("2ed1411d-6900-4707-90db-27fee490b6e3"), 24309 },
                    { new Guid("380c021d-596c-406d-a276-d56f6f41ad94"), 28159 },
                    { new Guid("38ae2ebb-d80f-48e4-81d4-21f20df776ad"), 17364 },
                    { new Guid("44efc62c-68de-4a1f-b7b3-3dbea7df5b7a"), 31519 },
                    { new Guid("471218b2-6461-4042-83e6-2944d24f325b"), 15635 },
                    { new Guid("4866ef2f-e1a5-436c-b619-0a436c00051a"), 33311 },
                    { new Guid("50e125eb-89d3-429a-865e-855c2e87f864"), 13663 },
                    { new Guid("545ab8bc-76f0-41f9-a2bc-cf87a4e63b62"), 16988 },
                    { new Guid("59c3aaa9-91f2-4684-85f3-d3efb52c73f2"), 11040 },
                    { new Guid("5b78dcfd-710d-42d2-bbce-b9afb705c442"), 11196 },
                    { new Guid("5d51f6c1-3270-4c6d-b63e-70a7694415b1"), 33047 },
                    { new Guid("6b4c2eb8-669b-4c17-aea6-5df72a236a85"), 22415 },
                    { new Guid("6bb7c97d-2841-484a-910c-32969ef23636"), 20766 },
                    { new Guid("6f1ea977-104a-43b7-bc10-ec39aea1cd20"), 19664 },
                    { new Guid("73f6f9e5-7afb-4699-a3e1-45b5fa45922e"), 11040 },
                    { new Guid("784cd549-04d9-4ac4-b649-62b2085ab72e"), 32183 },
                    { new Guid("8755e6d9-50db-440c-968c-609a34c72d86"), 20961 },
                    { new Guid("9307bae3-ce44-4853-823a-80ff412a4fc4"), 19477 },
                    { new Guid("95f5e766-b72d-4e95-8139-607b1c36398e"), 32828 },
                    { new Guid("962f40e7-2c85-45ff-ad3f-867db2c41e3a"), 12085 },
                    { new Guid("a502e588-8e5a-474b-8c46-afe7b07e6cfc"), 27890 },
                    { new Guid("aa9c9cba-1cb4-41ef-bb47-4edc0d8f74a5"), 33631 },
                    { new Guid("b5c574c6-4f01-4b80-8b67-5ec5e1794458"), 19455 },
                    { new Guid("b6b57aa9-1145-46e5-9720-5ae75ddd44a2"), 14108 },
                    { new Guid("c4d1ed7c-cfd7-4a29-ac39-d04f27162506"), 32884 },
                    { new Guid("c65199a8-b5b2-4452-9f04-be0022d0bab0"), 30001 },
                    { new Guid("c915ed0e-0358-4a2f-a84e-b21400ffd71b"), 30719 },
                    { new Guid("c98d335c-3665-443a-bda1-17cf788eb76e"), 26370 },
                    { new Guid("cc528f66-c65c-4fcb-8bda-44ca168dd0dd"), 25682 },
                    { new Guid("cf77c942-7e61-449a-b975-172fe30aae6d"), 32174 },
                    { new Guid("d14c5914-4e82-421b-a8f0-a174bd14c20e"), 22924 },
                    { new Guid("dbd3df4c-1b71-47ee-aee2-96e6d57f7602"), 21113 },
                    { new Guid("dbd99461-8db0-448a-8062-9d80fa6d1d68"), 32439 },
                    { new Guid("dfc4f83d-0945-452a-8e31-a96fa4868802"), 12916 },
                    { new Guid("e1e2c722-e2f7-45c2-b3d0-914c364c2dc6"), 12447 },
                    { new Guid("eefcfd2f-1078-4684-8922-a55d71e35d62"), 24409 },
                    { new Guid("f0332a9b-1378-4ea7-809a-6eeab707f1e6"), 32321 },
                    { new Guid("f063a19a-c255-4ff8-8c92-7b1753be5387"), 24628 },
                    { new Guid("f1c34437-ec0f-4c2f-97dd-c531b009fc66"), 16692 },
                    { new Guid("f2c607fd-5fbd-45ea-8019-2381c17b22fd"), 10847 },
                    { new Guid("f3db3695-90e7-4077-aeb9-9b8d5f617d02"), 34360 },
                    { new Guid("f40f3593-47de-49c0-a37b-62cdd81ae77f"), 29986 },
                    { new Guid("fccd3a2e-6418-4f9a-a45e-405348ecdcdf"), 12288 }
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
