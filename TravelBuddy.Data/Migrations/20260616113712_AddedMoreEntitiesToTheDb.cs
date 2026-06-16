using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreEntitiesToTheDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "excursions",
                columns: new[] { "Id", "Capacity", "Destination", "EndDate", "ImageUrl", "IsActive", "Price", "StartDate", "Title" },
                values: new object[,]
                {
                    { new Guid("0421801c-e358-45e8-a50b-90b0dbf4b220"), 35, "Florence", new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1578347995781-4210f1d1453f?w=600&auto=format&fit=crop", true, 1000.00m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "The Magic Of Florence" },
                    { new Guid("1d1c7ddb-d496-463c-8be1-c5b060e80ea6"), 15, "Kavala", new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1692057604287-53oNMGCRBMg?w=600&auto=format&fit=crop", true, 800.00m, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Summer in Kavala" },
                    { new Guid("6ae52e99-8f0b-4648-888b-e87302dbfc09"), 25, "Paris", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1538677446782-3f7a8d2e5b7d?w=600&auto=format&fit=crop", true, 2000.00m, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Disneyland" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("0421801c-e358-45e8-a50b-90b0dbf4b220"));

            migrationBuilder.DeleteData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("1d1c7ddb-d496-463c-8be1-c5b060e80ea6"));

            migrationBuilder.DeleteData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("6ae52e99-8f0b-4648-888b-e87302dbfc09"));
        }
    }
}
