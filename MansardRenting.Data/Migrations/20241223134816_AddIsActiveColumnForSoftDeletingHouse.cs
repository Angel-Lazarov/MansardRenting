using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MansardRenting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveColumnForSoftDeletingHouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("0f61dba5-df2c-46d1-9a02-70f0177dd948"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("3e7b6d28-41d8-4adf-880e-e71894e37ab2"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("ddd2f8dc-8a02-42af-8299-e3606cb6deba"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[,]
                {
                    { new Guid("40cd8089-9bcd-42a3-b3f2-c6b67c388e8e"), "North London, UK (near the border)", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.pleionrealestate.com/wp-content/uploads/2023/09/1695372772095-1048cf28-f176-43c8-932e-a75ab6f6ee02_12.jpg", 2100.00m, new Guid("863c6200-da00-4cc4-20f1-08dd20683655"), "Big House Marina" },
                    { new Guid("7f08257d-9b8e-430e-9db1-fb9b0011c193"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" },
                    { new Guid("ee4ff2d4-7797-4595-8111-16df85d3cac3"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("40cd8089-9bcd-42a3-b3f2-c6b67c388e8e"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("7f08257d-9b8e-430e-9db1-fb9b0011c193"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("ee4ff2d4-7797-4595-8111-16df85d3cac3"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Houses");

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[,]
                {
                    { new Guid("0f61dba5-df2c-46d1-9a02-70f0177dd948"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" },
                    { new Guid("3e7b6d28-41d8-4adf-880e-e71894e37ab2"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 2, "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1", 1200.00m, null, "Family House Comfort" },
                    { new Guid("ddd2f8dc-8a02-42af-8299-e3606cb6deba"), "North London, UK (near the border)", new Guid("fe5aff5f-9dc7-4a62-8384-199cdc007836"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.pleionrealestate.com/wp-content/uploads/2023/09/1695372772095-1048cf28-f176-43c8-932e-a75ab6f6ee02_12.jpg", 2100.00m, new Guid("863c6200-da00-4cc4-20f1-08dd20683655"), "Big House Marina" }
                });
        }
    }
}
