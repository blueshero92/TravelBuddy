using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitiesToTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(750)",
                maxLength: 750,
                nullable: false,
                defaultValue: "",
                comment: "The full name of the user.");

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "The title of the excursion."),
                    Destination = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "The destination of the excursion."),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Start date of the excursion."),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "End date of the excursion."),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the excursion."),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Free tourist spots available for the excursion."),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the excursion is active.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "The content of the notification message."),
                    SentOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the notification was sent."),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the notification has been read by the user."),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the user who received the notification.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the user who made the booking."),
                    ExcursionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the excursion associated with the booking."),
                    BookedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date when the booking was made."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Status of the booking.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the user who added the excursion to favorites."),
                    ExcursionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the excursion that was added to favorites.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.UserId, x.ExcursionId });
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingCancellationRequests",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the user who made the cancellation request."),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the booking associated with the cancellation request."),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of the cancellation request."),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "Reason for the cancellation request."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Status of the cancellation request."),
                    ReviewedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date when the cancellation request was reviewed by an administrator.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCancellationRequests", x => new { x.UserId, x.BookingId });
                    table.ForeignKey(
                        name: "FK_BookingCancellationRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BookingCancellationRequests_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCancellationRequests_BookingId",
                table: "BookingCancellationRequests",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ExcursionId",
                table: "Bookings",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ExcursionId",
                table: "Favorites",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCancellationRequests");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
