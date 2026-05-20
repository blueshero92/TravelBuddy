using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExcursionsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Excursions",
                columns: new[] { "Id", "Capacity", "Destination", "EndDate", "ImageUrl", "IsActive", "Price", "StartDate", "Title" },
                values: new object[,]
                {
                    { new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"), 25, "New York City", new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/images/city-exploration.jpg", true, 1000.00m, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "City Exploration" },
                    { new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"), 15, "Swiss Alps", new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/images/mountain-adventure.jpg", true, 1200.00m, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mountain Adventure" },
                    { new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"), 20, "Maldives", new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/images/beach-paradise.jpg", true, 1500.00m, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beach Paradise" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"));

            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"));

            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"));
        }
    }
}
