using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order_service.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LocalPart = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Complete = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    CountryCallingCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountOfStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    OrderedAmount = table.Column<int>(nullable: false),
                    ShippingDate = table.Column<DateTime>(nullable: false),
                    OrderId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName", "Country", "HouseNumber", "PostalCode", "StreetName", "Complete", "Domain", "LocalPart", "CountryCallingCode", "Number" },
                values: new object[] { new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e"), "Tom", "Thompson", "Belgium", "16A", "3000", "Jantjesstraat", "niels@mymail.be", "mymail.be", "niels", "+32", "484554433" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AmountOfStock", "Description", "Name", "Amount" },
                values: new object[] { new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), 30, "TestDescription", "TestName", 49.95m });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AmountOfStock", "Description", "Name", "Amount" },
                values: new object[] { new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), 30, "TestDescription", "TestName", 49.95m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId" },
                values: new object[] { new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), new Guid("d9a8b2a9-89c2-4b71-a7ae-def9f842b03e") });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ItemId", "OrderId1", "OrderedAmount", "ShippingDate", "Amount" },
                values: new object[] { new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), new Guid("712951fe-a3e7-4a1a-b09b-d8f8635b94f2"), null, 10, new DateTime(2018, 11, 23, 8, 53, 26, 363, DateTimeKind.Local), 49.95m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ItemId", "OrderId1", "OrderedAmount", "ShippingDate", "Amount" },
                values: new object[] { new Guid("7e7c06dc-6439-4951-ab30-a171da675929"), new Guid("d54b368d-3a7d-4063-bb02-6294c680849e"), null, 10, new DateTime(2018, 11, 23, 8, 53, 26, 368, DateTimeKind.Local), 49.95m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId1",
                table: "OrderItems",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
