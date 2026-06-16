using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExcursionsDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("0421801c-e358-45e8-a50b-90b0dbf4b220"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Florence, Italy", "/images/excursions/florence.jpg" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "New York City, USA", "/images/excursions/nyc.jpg" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("1d1c7ddb-d496-463c-8be1-c5b060e80ea6"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Kavala, Greece", "/images/excursions/kavala-beach.jpg" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("6ae52e99-8f0b-4648-888b-e87302dbfc09"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Paris, France", "/images/excursions/disneyland.jpg" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Swiss Alps, Switzerland", "/images/excursions/alps-mountains.jpg" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                column: "ImageUrl",
                value: "/images/excursions/maldives-beach.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("0421801c-e358-45e8-a50b-90b0dbf4b220"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Florence", "https://images.unsplash.com/photo-1578347995781-4210f1d1453f?w=600&auto=format&fit=crop" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "New York City", "https://images.unsplash.com/photo-1499092346589-b9b6be3e94b2?w=600&auto=format&fit=crop" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("1d1c7ddb-d496-463c-8be1-c5b060e80ea6"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Kavala", "https://images.unsplash.com/photo-1692057604287-53oNMGCRBMg?w=600&auto=format&fit=crop" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("6ae52e99-8f0b-4648-888b-e87302dbfc09"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Paris", "https://images.unsplash.com/photo-1538677446782-3f7a8d2e5b7d?w=600&auto=format&fit=crop" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                columns: new[] { "Destination", "ImageUrl" },
                values: new object[] { "Swiss Alps", "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=600&auto=format&fit=crop" });

            migrationBuilder.UpdateData(
                table: "excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=600&auto=format&fit=crop");
        }
    }
}
