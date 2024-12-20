using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MansardRenting.Data.Migrations
{
	/// <inheritdoc />
	public partial class SeedDb : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "Id", "Name" },
				values: new object[,]
				{
					{ 1, "Cottage" },
					{ 2, "Single-Family" },
					{ 3, "Duplex" }
				});

			migrationBuilder.InsertData(
				table: "Houses",
				columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
				values: new object[,]
				{
					{ new Guid("34ad8643-b5a0-4f9b-8553-d655b40c1eac"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" },
					{ new Guid("9b14c109-c164-4b2d-b363-c76e98048245"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" },
					{ new Guid("b2e032a8-7b35-4dfd-93f9-c024e9642227"), "North London, UK (near the border)", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("863C6200-DA00-4CC4-20F1-08DD20683655"), "Big House Marina" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "Houses",
				keyColumn: "Id",
				keyValue: new Guid("34ad8643-b5a0-4f9b-8553-d655b40c1eac"));

			migrationBuilder.DeleteData(
				table: "Houses",
				keyColumn: "Id",
				keyValue: new Guid("9b14c109-c164-4b2d-b363-c76e98048245"));

			migrationBuilder.DeleteData(
				table: "Houses",
				keyColumn: "Id",
				keyValue: new Guid("b2e032a8-7b35-4dfd-93f9-c024e9642227"));

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 2);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "Id",
				keyValue: 3);
		}
	}
}
