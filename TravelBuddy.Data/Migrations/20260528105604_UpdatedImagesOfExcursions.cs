using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImagesOfExcursions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1499092346589-b9b6be3e94b2?w=600&auto=format&fit=crop");

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=600&auto=format&fit=crop");

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=600&auto=format&fit=crop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                column: "ImageUrl",
                value: "https://example.com/images/city-exploration.jpg");

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                column: "ImageUrl",
                value: "https://example.com/images/mountain-adventure.jpg");

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                column: "ImageUrl",
                value: "https://example.com/images/beach-paradise.jpg");
        }
    }
}
