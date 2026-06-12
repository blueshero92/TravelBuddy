using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddy.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntitialPostgreMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aspnetroles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(750)", maxLength: 750, nullable: true, comment: "The full name of the user."),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetusers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "excursions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "The title of the excursion."),
                    Destination = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false, comment: "The destination of the excursion."),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Start date of the excursion."),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "End date of the excursion."),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false, comment: "Price of the excursion."),
                    Capacity = table.Column<int>(type: "integer", nullable: false, comment: "Free tourist spots available for the excursion."),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, comment: "Indicates whether the excursion is active."),
                    ImageUrl = table.Column<string>(type: "text", nullable: true, comment: "URL of the excursion image.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_excursions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetroleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnetroleclaims_aspnetroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnetroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserclaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aspnetuserclaims_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_aspnetuserlogins_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserroles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_aspnetuserroles_aspnetroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "aspnetroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aspnetuserroles_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusertokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetusertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_aspnetusertokens_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false, comment: "The content of the notification message."),
                    SentOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "The date and time when the notification was sent."),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, comment: "Indicates whether the notification has been read by the user."),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the user who received the notification.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the user who made the booking."),
                    ExcursionId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the excursion associated with the booking."),
                    BookedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Date when the booking was made."),
                    Status = table.Column<int>(type: "integer", nullable: false, comment: "Status of the booking.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookings_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the user who added the excursion to favorites."),
                    ExcursionId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the excursion that was added to favorites.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => new { x.UserId, x.ExcursionId });
                    table.ForeignKey(
                        name: "FK_favorites_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favorites_excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookingcancellationrequests",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the user who made the cancellation request."),
                    BookingId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id of the booking associated with the cancellation request."),
                    RequestedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Date of the cancellation request."),
                    Reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true, comment: "Reason for the cancellation request."),
                    Status = table.Column<int>(type: "integer", nullable: false, comment: "Status of the cancellation request."),
                    ReviewedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Date when the cancellation request was reviewed by an administrator.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingcancellationrequests", x => new { x.UserId, x.BookingId });
                    table.ForeignKey(
                        name: "FK_bookingcancellationrequests_aspnetusers_UserId",
                        column: x => x.UserId,
                        principalTable: "aspnetusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookingcancellationrequests_bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "excursions",
                columns: new[] { "Id", "Capacity", "Destination", "EndDate", "ImageUrl", "IsActive", "Price", "StartDate", "Title" },
                values: new object[,]
                {
                    { new Guid("178a0c9f-75b7-4c90-9e72-53922dfa72e4"), 25, "New York City", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1499092346589-b9b6be3e94b2?w=600&auto=format&fit=crop", true, 1000.00m, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), "City Exploration" },
                    { new Guid("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"), 15, "Swiss Alps", new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=600&auto=format&fit=crop", true, 1200.00m, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Mountain Adventure" },
                    { new Guid("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"), 20, "Maldives", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=600&auto=format&fit=crop", true, 1500.00m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Beach Paradise" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_aspnetroleclaims_RoleId",
                table: "aspnetroleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnetroles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserclaims_UserId",
                table: "aspnetuserclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserlogins_UserId",
                table: "aspnetuserlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserroles_RoleId",
                table: "aspnetuserroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "aspnetusers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "aspnetusers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookingcancellationrequests_BookingId",
                table: "bookingcancellationrequests",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookings_ExcursionId",
                table: "bookings",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId",
                table: "bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_ExcursionId",
                table: "favorites",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_UserId",
                table: "notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnetroleclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserlogins");

            migrationBuilder.DropTable(
                name: "aspnetuserroles");

            migrationBuilder.DropTable(
                name: "aspnetusertokens");

            migrationBuilder.DropTable(
                name: "bookingcancellationrequests");

            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "aspnetroles");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "aspnetusers");

            migrationBuilder.DropTable(
                name: "excursions");
        }
    }
}
