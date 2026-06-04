using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatesForExcursions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
